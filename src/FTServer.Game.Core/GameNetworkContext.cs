using FTServer.Database.Model;
using FTServer.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FTServer.Game.Core
{
    public class GameNetworkContext : NetworkContext<GameNetworkContext>
    {
        private readonly IConcurrentUserTrackingService _concurrentUserTrackingService;
        public GameState State { get; set; }
        public GameNetworkContext(NetworkContextOptions contextOptions, IServiceProvider serviceProvider,
            IConcurrentUserTrackingService concurrentUserTrackingService) : base(contextOptions, serviceProvider)
        {
            _concurrentUserTrackingService = concurrentUserTrackingService;
            State = GameState.Authenticate;
        }

        public Character Character { get; set; }

        protected override async Task Connected()
        {
            await _concurrentUserTrackingService.Increment();
            await UseBlowfishCryptography();
        }

        protected override async Task Disconnected()
        {
            State = GameState.Offline;
            await _concurrentUserTrackingService.Decrement();
        }
        public async Task<bool> FaultyState(GameState expected)
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
        public async Task<bool> FaultyMinimumState(GameState minimum)
        {
            if (State <= minimum)
            {
                Logger.LogDebug($"{Options.RemoteEndPoint} disconnected in wrong state. Expected {minimum}+ but found {State}");
                Logger.LogTrace($"{Options.RemoteEndPoint} disconnected in wrong state.\n{Environment.StackTrace}");
                await DisconnectAsync();
                return true;
            }
            return false;
        }
    }
}
