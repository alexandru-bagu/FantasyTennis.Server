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
using FTServer.Network.Message.System.Session;
using System.Security.Cryptography;

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
        private readonly ICryptographyServiceFactory _cryptographyServiceFactory;

        private TNetworkContext _self;
        private bool _alignBuffer;
        private bool _saidHello;
        private bool _connected;

        protected ICryptographyService CryptographyService { get; set; }
        protected IMessageSerialService MessageSerialService { get; set; }
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

            Options = contextOptions;
            Logger = loggerFactory.CreateLogger<NetworkContext<TNetworkContext>>();
            MessageSerialService = serviceProvider.GetService<IMessageSerialService>();
            CryptographyService = new PlainCryptographyService();

            _networkMessageFactory = serviceProvider.GetService<INetworkMessageFactory>();
            _networkMessageHandlerService = serviceProvider.GetService<INetworkMessageHandlerService<TNetworkContext>>();
            _messageChecksumService = serviceProvider.GetService<IMessageChecksumService>();
            _cryptographyServiceFactory = serviceProvider.GetService<ICryptographyServiceFactory>();

            _self = (TNetworkContext)(object)this;
            _alignBuffer = false;
            _connected = true;
            _saidHello = false;
        }

        ~NetworkContext()
        {
            if (_connected)
                InternalDisconnect();
        }

        /// <summary>
        /// Invoked on connect. Must one of the cryptography methods: UseBlowfishCryptography, UseXorCryptography or UsePlainCryptography.
        /// </summary>
        protected abstract Task Connected();

        /// <summary>
        /// Invoked on disconnect.
        /// </summary>
        protected abstract Task Disconnected();

        public virtual async Task SendAsync(INetworkMessage message)
        {
            var buffer = ArrayPool<byte>.Shared.Rent(message.MaximumSize);
            try
            {
                int size = message.Serialize(buffer, 0);
                await SendRawAsync(buffer, 0, size);
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
                int align = _alignBuffer ? 8 - (size % 8) : 0;
                var local = ArrayPool<byte>.Shared.Rent(size + align + (_alignBuffer ? 1 : 0));
                Buffer.BlockCopy(buffer, offset, local, 0, size);
                if (!_sendStopwatch.IsRunning || (_sendStopwatch.IsRunning && _sendStopwatch.ElapsedMilliseconds <= NetworkCongestionTimeout))
                {
                    var _ = ProcessSend(local, 0, size, align);
                }
                else
                {
                    await DisconnectAsync();
                    Logger.LogInformation("SendRawAsync");
                }
            }
        }

        private async Task ProcessSend(byte[] buffer, int offset, int size, int alignmentSize)
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
                        header->Serial = MessageSerialService.ComputeSend(buffer, offset);
                        header->Checksum = _messageChecksumService.Compute(buffer, offset);
                        header->BodyLength += (ushort)alignmentSize;
                    }
                }
#if DEBUG
                LogPacket(buffer, offset, size, "Send");
#endif
                CryptographyService.Encrypt(buffer, offset, size + alignmentSize);
                if (_alignBuffer) buffer[offset + alignmentSize + size] = (byte)(alignmentSize ^ buffer[3]);
                await _connection.WriteAsync(buffer, offset, size + alignmentSize + (_alignBuffer ? 1 : 0));
            }
            catch (SocketException ex)
            {
                await DisconnectAsync();
                Logger.LogDebug(ex, "ProcessSend");
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(buffer);
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
                    var encryptedChecksumByte = _readBuffer[3];
                    CryptographyService.Decrypt(_readBuffer, 0, 8);
                    ushort length = (ushort)(_readBuffer[6] | (_readBuffer[7] << 8));
                    await ReadBlock(8, length + (_alignBuffer ? 1 : 0));
                    if (length >= _readBuffer.Length) throw new Exception("Desynchronization error.");
                    CryptographyService.Decrypt(_readBuffer, 8, length);

                    if (_alignBuffer)
                    {
                        var v = (byte)(encryptedChecksumByte ^ _readBuffer[8 + length]);
                        length -= v;
                        _readBuffer[6] = (byte)(length & 0xFF);
                        _readBuffer[7] = (byte)((length >> 8) & 0xFF);
                    }

                    ushort recvChecksum = (ushort)(_readBuffer[2] | (_readBuffer[3] << 8));
                    if (_messageChecksumService.Validate(_readBuffer, 0, recvChecksum))
                    {
#if DEBUG
                        LogPacket(_readBuffer, 0, length + 8, "{V} Recv");
#endif
                        var _ = ProcessReceive(_networkMessageFactory.Create(_readBuffer, 0, length + 8));
                    }
                    else
                    {
#if DEBUG
                        LogPacket(_readBuffer, 0, length + 8, "{I} Recv");
#endif
                    }
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

#if DEBUG
        private void LogPacket(byte[] buffer, int offset, int length, string direction)
        {
            Logger.LogTrace($"{direction}: {BitConverter.ToString(buffer, offset, length).Replace("-", " ")}");
        }
#endif

        protected virtual async Task ProcessReceive(INetworkMessage message)
        {
            await _processRead.WaitAsync();
            try
            {
                if (MessageSerialService.ValidateReceive(message.Header.Serial))
                {
                    await _networkMessageHandlerService.Process(message, _self);
                }
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

        async Task INetworkContextNotification.NotifyConnected()
        {
            await Connected();
            var _ = NetworkReceive();
        }
        async Task INetworkContextNotification.NotifyDisconnected() { await Disconnected(); }


        protected async Task UseBlowfishCryptography()
        {
            if (_saidHello) throw new Exception("Already said hello.");
            _saidHello = true;
            var bfEncKey = new byte[16];
            var bfDecKey = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bfEncKey);
                rng.GetBytes(bfDecKey);
            }
            await SendAsync(new BlowfishSessionHello(bfDecKey, bfEncKey));
            CryptographyService = _cryptographyServiceFactory.CreateBlowfish(new CryptographicOption(bfDecKey), new CryptographicOption(bfEncKey));
            _alignBuffer = true;
        }

        protected async Task UseXorCryptography()
        {
            if (_saidHello) throw new Exception("Already said hello.");
            _saidHello = true;
            var rnd = new Random();
            var bfEncKey = rnd.Next();
            var bfDecKey = rnd.Next();
            var sendSerialKey = rnd.Next(0, 5);
            var receiveSerialKey = rnd.Next(0, 5);
            await SendAsync(new XorSessionHello(bfDecKey, bfEncKey, sendSerialKey, receiveSerialKey));
            MessageSerialService.UpdateKeys(receiveSerialKey, sendSerialKey);
            CryptographyService = _cryptographyServiceFactory.CreateXor(new CryptographicOption(BitConverter.GetBytes(bfDecKey)), new CryptographicOption(BitConverter.GetBytes(bfEncKey)));
        }

        protected async Task UsePlainCryptography()
        {
            if (_saidHello) throw new Exception("Already said hello.");
            _saidHello = true;
            await SendAsync(new PlainSessionHello());
        }
    }
}
