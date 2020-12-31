using FTServer.Database.Model;
using FTServer.Dto;
using FTServer.Network;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FTServer.Game.Core
{
    public class GameNetworkContext : NetworkContext<GameNetworkContext>
    {
        private readonly IConcurrentUserTrackingService _concurrentUserTrackingService;
        private readonly SemaphoreSlim _semaphoreSlim;
        public GameState State { get; set; }
        public GameNetworkContext(NetworkContextOptions contextOptions, IServiceProvider serviceProvider,
            IConcurrentUserTrackingService concurrentUserTrackingService) : base(contextOptions, serviceProvider)
        {
            _concurrentUserTrackingService = concurrentUserTrackingService;
            _semaphoreSlim = new SemaphoreSlim(1);
            State = GameState.Authenticate;
        }

        public Character Character { get; set; }
        public Home Home { get; set; }
        public ConcurrentList<Item> Items { get; set; }
        public ConcurrentList<FriendDto> Friends { get; set; }

        public int Ap { get { return Character.Account.Ap; } }
        public int Gold { get { return Character.Gold; } }
        public int CharmPoints { get { return Character.CharmPoints; } }

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

        public bool HasAp(int amount)
        {
            return Character.Account.Ap >= amount;
        }
        public Task<bool> AddAp(int amount)
        {
            return AddCurrency(ap: amount);
        }

        public bool HasCharm(int amount)
        {
            return Character.CharmPoints >= amount;
        }
        public Task<bool> AddCharm(int amount)
        {
            return AddCurrency(charm: amount);
        }

        public bool HasGold(int amount)
        {
            return Character.Gold >= amount;
        }
        public Task<bool> AddGold(int amount)
        {
            return AddCurrency(gold: amount);
        }

        public async Task<bool> AddCurrency(int gold = 0, int ap = 0, int charm = 0)
        {
            if (Logger.IsEnabled(LogLevel.Debug))
                Logger.LogDebug($"[{Character.Id}] add currency {gold}gold, {ap}ap, {charm}charm");
            await _semaphoreSlim.WaitAsync();
            try
            {
                if (gold < 0 && !HasGold(-gold)) return false;
                if (ap < 0 && !HasAp(-ap)) return false;
                if (charm < 0 && !HasCharm(-charm)) return false;
                await using (var uow = UnitOfWorkFactory.Create())
                {
                    uow.Attach(Character);
                    Character.Gold += gold;
                    Character.Account.Ap += ap;
                    Character.CharmPoints += charm;
                    await uow.CommitAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"[{Character.Id}] add currency {gold}gold, {ap}ap, {charm}charm");
            }
            finally
            {
                if (Logger.IsEnabled(LogLevel.Debug))
                    Logger.LogDebug($"[{Character.Id}] has {Character.Gold}gold, {Character.Account.Ap}ap, {Character.CharmPoints}charm");
                _semaphoreSlim.Release();
            }
            return false;
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
