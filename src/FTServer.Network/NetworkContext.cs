using FTServer.Contracts.Network;
using FTServer.Contracts.Security;
using FTServer.Contracts.Services.Network;
using FTServer.Security;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Buffers;
using System.Diagnostics;
using System.Net;

namespace FTServer.Network
{
    public abstract class NetworkContext<TNetworkContext> : INetworkContext
        where TNetworkContext : INetworkContext
    {
        public const int NetworkCongestionTimeout = 5000;
        private readonly Stream _connection;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly SemaphoreSlim _processRead, _processSend;
        private readonly byte[] _readBuffer;
        private readonly Stopwatch _sendStopwatch;
        private readonly INetworkMessageFactory _networkMessageFactory;
        private readonly INetworkMessageHandlerService<TNetworkContext> _networkMessageHandlerService;
        private readonly IMessageChecksumService _messageChecksumService;
        private readonly IMessageSerialService _messageSerialService;
        private TNetworkContext _self;

        private bool _connected;

        protected ICryptographyService CryptographyService { get; set; }
        protected ILogger Logger { get; private set; }
        public INetworkContextOptions Options { get; }

        public NetworkContext(NetworkContextOptions contextOptions, IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            _connection = contextOptions.Stream;
            _readBuffer = ArrayPool<byte>.Shared.Rent(4096);
            _cancellationTokenSource = new CancellationTokenSource();
            _processRead = new SemaphoreSlim(1);
            _processSend = new SemaphoreSlim(1);
            _sendStopwatch = new Stopwatch();

            _connected = true;

            Options = contextOptions;
            Logger = loggerFactory.CreateLogger<NetworkContext<TNetworkContext>>();
            CryptographyService = new NullCryptographyService();

            _networkMessageFactory = serviceProvider.GetService<INetworkMessageFactory>();
            _networkMessageHandlerService = serviceProvider.GetService<INetworkMessageHandlerService<TNetworkContext>>();
            _messageChecksumService = serviceProvider.GetService<IMessageChecksumService>();
            _messageSerialService = serviceProvider.GetService<IMessageSerialService>();

            _self = (TNetworkContext)((object)this);
        }

        ~NetworkContext()
        {
            if (_connected)
                InternalDisconnect();
        }

        protected abstract Task Connected();
        protected abstract Task Disconnected();

        public virtual async Task SendAsync(INetworkMessage message)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(message.MaximumSize);
            try
            {
                int size = message.Serialize(buffer, 0);
                await ((IRawNetworkContext)this).SendRawAsync(buffer, 0, size);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
            }
        }

        public virtual async Task SendRawAsync(byte[] buffer, int offset, int size)
        {
            if (_connected)
            {
                var local = ArrayPool<byte>.Shared.Rent(size);
                Buffer.BlockCopy(buffer, offset, local, 0, size);
                if (!_sendStopwatch.IsRunning || (_sendStopwatch.IsRunning && _sendStopwatch.ElapsedMilliseconds <= NetworkCongestionTimeout))
                {
                    var _ = ProcessSend(local, 0, size);
                }
                else
                {
                    await DisconnectAsync();
                    Logger.LogInformation("SendRawAsync");
                }
            }
        }

        private async Task ProcessSend(byte[] buffer, int offset, int size)
        {
            await _processSend.WaitAsync();
            try
            {
                _sendStopwatch.Restart();
                unsafe
                {
                    fixed (byte* ptr = buffer)
                    {
                        var header = (NetworkMessageHeader*)ptr;
                        header->Serial = _messageSerialService.ComputeSend(buffer, offset);
                        header->Checksum = _messageChecksumService.Compute(buffer, offset);
                    }
                }
                CryptographyService.Encrypt(buffer, offset, size);
                await _connection.WriteAsync(buffer, offset, size);
            }
            catch (SocketException ex)
            {
                await DisconnectAsync();
                Logger.LogDebug(ex, "ProcessSend");
            }
            finally
            {
                _sendStopwatch.Stop();
                _processSend.Release();
            }
        }

        private void InternalDisconnect()
        {
            if (_connected)
            {
                _connected = false;
                _cancellationTokenSource.Cancel();
                _connection.Dispose();
                _processRead.Dispose();
                _processSend.Dispose();
                ArrayPool<byte>.Shared.Return(_readBuffer);
            }
        }

        public async Task DisconnectAsync()
        {
            try { await _connection.FlushAsync(); }
            catch { /* Ignore errors as we are going to ignore already disconnected devices */ }

            if (_connected)
            {
                InternalDisconnect();
                await ((INetworkContextNotification)this).NotifyDisconnected();
            }
        }

        private async Task ReadBlock(int offset, int size)
        {
            int recv = 0;
            while (recv < size)
            {
                var sz = await _connection.ReadAsync(_readBuffer, offset + recv, size - recv, _cancellationTokenSource.Token);
                if (sz == 0 && size - recv != 0) throw new SocketException((int)SocketError.Disconnecting);
                recv += sz;
            }
        }

        private async Task NetworkReceive()
        {
            try
            {
                while (!_cancellationTokenSource.IsCancellationRequested && _connected)
                {
                    await ReadBlock(0, 8);
                    CryptographyService.Decrypt(_readBuffer, 0, 8);
                    ushort length = (ushort)(_readBuffer[6] | (_readBuffer[7] << 8));
                    await ReadBlock(8, length);
                    CryptographyService.Decrypt(_readBuffer, 8, length);
                    var _ = ProcessReceive(_networkMessageFactory.Create(_readBuffer, 0, length + 8));
                }
            }
            catch (SocketException ex)
            {
                await DisconnectAsync();
                Logger.LogDebug(ex, "ProcessReceive");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ProcessReceive");
            }
        }

        protected virtual async Task ProcessReceive(INetworkMessage message)
        {
            await _processRead.WaitAsync();
            try
            {
                await _networkMessageHandlerService.Process(message, _self);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "ProcessMessage");
            }
            finally
            {
                _processRead.Release();
            }
        }

        Task INetworkContextNotification.NotifyConnected()
        {
            var _ = NetworkReceive();
            _ = Connected();
            return Task.CompletedTask;
        }
        async Task INetworkContextNotification.NotifyDisconnected() { await Disconnected(); }
    }
}
