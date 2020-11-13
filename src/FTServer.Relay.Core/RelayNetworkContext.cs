using FTServer.Contracts.Services.Network;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace FTServer.Relay.Core
{
    public class RelayNetworkContext : NetworkContext
    {
        public RelayNetworkContext(Socket connection, IServiceProvider serviceProvider) : base(connection)
        {
        }

        protected override Task Connected()
        {
            return Task.CompletedTask;
        }

        protected override Task DisconnectAsync()
        {
            return Task.CompletedTask;
        }

        protected override Task Disconnected()
        {
            return Task.CompletedTask;
        }

        protected override Task SendAsync()
        {
            return Task.CompletedTask;
        }
    }
}
