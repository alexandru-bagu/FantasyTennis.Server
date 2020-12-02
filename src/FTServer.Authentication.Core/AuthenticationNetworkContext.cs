using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core
{
    public class AuthenticationNetworkContext : NetworkContext<AuthenticationNetworkContext>
    {
        public AuthenticationState State { get; set; }

        public AuthenticationNetworkContext(NetworkContextOptions contextOptions, IServiceProvider serviceProvider) : base(contextOptions, serviceProvider)
        {
            State = AuthenticationState.Authenticate;
        }

        public Account Account { get; set; }

        public async Task<bool> FaultyState(AuthenticationState expected)
        {
            if (State != expected)
            {
                Logger.LogDebug($"{Options.RemoteEndPoint} disconnected in wrong state. Expected {expected} but found {State}");
                Logger.LogTrace($"{Options.RemoteEndPoint} disconnected in wrong state.\n{Environment.StackTrace}");
                await DisconnectAsync();
                return true;
            }
            return false;
        }

        protected override async Task Connected()
        {
            await UseBlowfishCryptography();
        }

        protected override async Task Disconnected()
        {
            State = AuthenticationState.Offline;
            await Task.CompletedTask;
        }
    }
}
