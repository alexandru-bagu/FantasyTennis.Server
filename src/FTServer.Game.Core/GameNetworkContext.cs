using FTServer.Contracts.Security;
using FTServer.Contracts.Services.Network;
using System.IO;
using System.Threading.Tasks;

namespace FTServer.Game.Core
{
    public class GameNetworkContext : NetworkContext
    {
        public GameNetworkContext(Stream connection, ICryptographicServiceFactory cryptographicServiceFactory) : base(connection, cryptographicServiceFactory)
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
