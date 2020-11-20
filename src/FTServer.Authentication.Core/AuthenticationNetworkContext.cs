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
            await SendAsync(new BlowfishSession(new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }, new byte[16] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 }));
        }

        protected override Task Disconnected()
        {
            return Task.CompletedTask;
        }
    }
}
