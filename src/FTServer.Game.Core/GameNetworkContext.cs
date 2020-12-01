using FTServer.Database.Model;
using FTServer.Network;
using System;
using System.Threading.Tasks;

namespace FTServer.Game.Core
{
    public class GameNetworkContext : NetworkContext<GameNetworkContext>
    {
        private readonly IConcurrentUserTrackingService _concurrentUserTrackingService;

        public GameNetworkContext(NetworkContextOptions contextOptions, IServiceProvider serviceProvider,
            IConcurrentUserTrackingService concurrentUserTrackingService) : base(contextOptions, serviceProvider)
        {
            _concurrentUserTrackingService = concurrentUserTrackingService;
        }

        public Character Character { get; set; }

        protected override async Task Connected()
        {
            await _concurrentUserTrackingService.Increment();
            await UseXorCryptography();
        }

        protected override async Task Disconnected()
        {
            await _concurrentUserTrackingService.Decrement(); 
        }
    }
}
