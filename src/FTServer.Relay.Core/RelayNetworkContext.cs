using FTServer.Network;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FTServer.Relay.Core
{
    public class RelayNetworkContext : NetworkContext<RelayNetworkContext>
    {
        public RelayNetworkContext(NetworkContextOptions contextOptions,  IServiceProvider serviceProvider) : base(contextOptions, serviceProvider)
        {
        }

        protected override Task Connected()
        {
            return Task.CompletedTask;
        }

        protected override Task Disconnected()
        {
            return Task.CompletedTask;
        }
    }
}
