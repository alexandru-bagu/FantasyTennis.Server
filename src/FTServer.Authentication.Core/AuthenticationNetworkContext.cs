using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
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

        public Account Account { get; set; }

        protected override async Task Connected()
        {
            await UseBlowfishCryptography();
        }

        protected override async Task Disconnected()
        {
            await Task.CompletedTask;
        }
    }
}
