using FTServer.Contracts.Network;
using FTServer.Contracts.Security;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Network
{
    public abstract class NetworkContext : INetworkConnection, INetworkConnectionNotification
    {
        private readonly Stream _connection;
        private readonly ICryptographicService _cryptographyService;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private byte[] _readBuffer;
        public NetworkContext(Stream connection, ICryptographicServiceFactory cryptographicServiceFactory)
        {
            _connection = connection;
            _cryptographyService = cryptographicServiceFactory.Create();
            _readBuffer = new byte[4096];
            _cancellationTokenSource = new CancellationTokenSource();
        }

        protected abstract Task Connected();
        protected abstract Task Disconnected();

        public Task SendAsync(INetworkMessage message)
        {
            return Task.CompletedTask;
        }

        Task IRawNetworkConnection.SendRawAsync(byte[] buffer, int offset, int size)
        {
            return Task.CompletedTask;
        }

        public Task DisconnectAsync()
        {
            _cancellationTokenSource.Cancel();
            _connection.Dispose();
            return Disconnected();
        }

        private async Task ReadBlock(int offset, int size)
        {
            Socket sx = null;
            sx.ReceiveAsync(new SocketAsyncEventArgs() { });
            int recv = 0;
            while (recv < size)
                recv += await _connection.ReadAsync(_readBuffer, offset + recv, size - recv, _cancellationTokenSource.Token);
        }

        private async Task ProcessReceive()
        {
            NetworkMessageHeader header;
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                await ReadBlock(0, 8);
                _cryptographyService.Decrypt(_readBuffer, 0, 8);
                unsafe { fixed (byte* ptr = _readBuffer) header = *(NetworkMessageHeader*)ptr; }
                await ReadBlock(8, header.Length);
                _cryptographyService.Decrypt(_readBuffer, 8, header.Length);
            }
        }

        async Task INetworkConnectionNotification.NotifyConnected()
        {
            await Task.Factory.StartNew(ProcessReceive, _cancellationTokenSource.Token);
            await Connected();
        }
        async Task INetworkConnectionNotification.NotifyDisconnected() { await Disconnected(); }
    }
}
