using FTServer.Contracts.Network;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FTServer.Relay.Core.Network
{
    public class DefaultNetworkMessageHandler : INetworkMessageHandler<RelayNetworkContext>
    {
        private readonly ILogger<DefaultNetworkMessageHandler> _logger;

        public DefaultNetworkMessageHandler(ILogger<DefaultNetworkMessageHandler> logger)
        {
            _logger = logger;
        }

        public Task Process(INetworkMessage message, RelayNetworkContext context)
        {
            return Task.CompletedTask;
        }
    }
}
