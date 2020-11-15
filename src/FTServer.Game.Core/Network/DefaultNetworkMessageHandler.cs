using FTServer.Contracts.Network;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network
{
    public class DefaultNetworkMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly ILogger<DefaultNetworkMessageHandler> _logger;

        public DefaultNetworkMessageHandler(ILogger<DefaultNetworkMessageHandler> logger)
        {
            _logger = logger;
        }

        public Task Process(INetworkMessage message, GameNetworkContext context)
        {
            return Task.CompletedTask;
        }
    }
}
