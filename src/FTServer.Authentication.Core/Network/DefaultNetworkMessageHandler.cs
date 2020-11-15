using FTServer.Contracts.Network;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    public class DefaultNetworkMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly ILogger<DefaultNetworkMessageHandler> _logger;

        public DefaultNetworkMessageHandler(ILogger<DefaultNetworkMessageHandler> logger)
        {
            _logger = logger;
        }

        public Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            return Task.CompletedTask;
        }
    }
}
