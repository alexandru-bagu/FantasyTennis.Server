using System.Net.Sockets;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Network
{
    public abstract class NetworkContext : INetworkConnection, INetworkConnectionNotification
    {
        private readonly Socket _connection;

        public NetworkContext(Socket connection)
        {
            _connection = connection;
        }

        protected abstract Task Connected();
        protected abstract Task Disconnected();

        protected abstract Task SendAsync();
        protected abstract Task DisconnectAsync();

        async Task INetworkConnectionNotification.NotifyConnected() { await Connected(); }
        async Task INetworkConnectionNotification.NotifyDisconnected() { await Disconnected(); }
    }
}
