using FTServer.Network;
using System;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core
{
    public class AuthenticationNetworkContext : NetworkContext<AuthenticationNetworkContext>
    {
        public AuthenticationNetworkContext(NetworkContextOptions contextOptions, IServiceProvider serviceProvider) : base(contextOptions, serviceProvider)
        {
        }

        protected override async Task Connected()
        {
            await UseBlowfishCryptography();
        }

        protected override Task Disconnected()
        {
            return Task.CompletedTask;
        }
    }
}
