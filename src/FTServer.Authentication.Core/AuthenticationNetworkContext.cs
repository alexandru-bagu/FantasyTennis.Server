using FTServer.Network;
using FTServer.Network.Message.Login;
using System;
using System.IO;
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
            await SendAsync(new WelcomeMessage(0, 0, 0, 0));
        }

        protected override Task Disconnected()
        {
            return Task.CompletedTask;
        }
    }
}
