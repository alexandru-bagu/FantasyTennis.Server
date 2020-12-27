using FTServer.Database.Model;
using FTServer.Dto;
using FTServer.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public Home Home { get; set; }
        public List<Item> Items { get; set; }
        public List<FriendDto> Friends { get; set; }

        protected override async Task Connected()
        {
            await _concurrentUserTrackingService.Increment();
            await UseBlowfishCryptography();
        }

        protected override async Task Disconnected()
        {
            State = GameState.Offline;
            await _concurrentUserTrackingService.Decrement();
            await using (var uow = UnitOfWorkFactory.Create())
            {
                uow.Attach(Character.Account);
                Character.Account.Online = false;
                Character.Account.ActiveServerId = null;
                await uow.CommitAsync();
            }

        }
        public async Task<bool> FaultyState(GameState expected)
        {
            if (State != expected)
            {
                if (Logger.IsEnabled(LogLevel.Debug))
                    Logger.LogDebug($"{Options.RemoteEndPoint} disconnected in wrong state. Expected {expected} but found {State}");
                if (Logger.IsEnabled(LogLevel.Trace))
                    Logger.LogTrace($"{Options.RemoteEndPoint} disconnected in wrong state.\n{Environment.StackTrace}");
                await DisconnectAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> FaultyMinimumState(GameState minimum)
        {
            if (State < minimum)
            {
                if (Logger.IsEnabled(LogLevel.Debug))
                    Logger.LogDebug($"{Options.RemoteEndPoint} disconnected in wrong state. Expected {minimum}+ but found {State}");
                if (Logger.IsEnabled(LogLevel.Trace))
                    Logger.LogTrace($"{Options.RemoteEndPoint} disconnected in wrong state.\n{Environment.StackTrace}");
                await DisconnectAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> FaultyState(bool assertion, string failure)
        {
            if (!assertion)
            {
                if (Logger.IsEnabled(LogLevel.Debug))
                    Logger.LogDebug($"{Options.RemoteEndPoint} disconnected in wrong state. {failure}");
                if (Logger.IsEnabled(LogLevel.Trace))
                    Logger.LogTrace($"{Options.RemoteEndPoint} disconnected in wrong state.\n{Environment.StackTrace}");
                await DisconnectAsync();
                return true;
            }
            return false;
        }
    }
}
