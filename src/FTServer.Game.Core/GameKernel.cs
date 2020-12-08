using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Services.Network;
using FTServer.Database.Model;
using FTServer.Game.Core.Network;
using FTServer.Game.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Game.Core
{
    public class GameKernel : BackgroundService
    {
        private readonly ILogger<GameKernel> _logger;
        private readonly INetworkServiceFactory _networkServiceFactory;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IConcurrentUserTrackingService _concurrentUserTrackingService;
        private readonly AppSettings _appSettings;
        private INetworkService<GameNetworkContext> _gameNetworkService;

        public GameKernel(ILogger<GameKernel> logger, IServiceProvider serviceProvider,
            INetworkServiceFactory networkServiceFactory,
            IOptions<AppSettings> appSettings,
            INetworkMessageHandlerService<GameNetworkContext> networkMessageHandlerService,
            IUnitOfWorkFactory unitOfWorkFactory,
            IConcurrentUserTrackingService concurrentUserTrackingService,
            Contracts.Stores.IItemDataStore resx)
        {
            _logger = logger;
            _networkServiceFactory = networkServiceFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _concurrentUserTrackingService = concurrentUserTrackingService;
            _appSettings = appSettings.Value;
            networkMessageHandlerService.RegisterDefaultHandler(serviceProvider.Create<DefaultNetworkMessageHandler>());

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                if (await IsServerEnabled())
                {
                    await _concurrentUserTrackingService.Reset();
                    using (_gameNetworkService = _networkServiceFactory.Create<GameNetworkContext>(_appSettings.GameServer.Network.Host, _appSettings.GameServer.Network.Port))
                    {
                        await _gameNetworkService.ListenAsync();

                        _logger.LogInformation("Game server started.");

                        while (!stoppingToken.IsCancellationRequested)
                        {
                            await Heartbeat();
                            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatal: {ex.Message}");
            }
        }

        private async Task<bool> IsServerEnabled()
        {
            GameServer server = null;
            await using (var uow = await _unitOfWorkFactory.Create())
            {
                server = await uow.GameServers.Where(p => p.Name == _appSettings.GameServer.Name).FirstOrDefaultAsync();
                if (server == null)
                {
                    server = new GameServer()
                    {
                        Name = _appSettings.GameServer.Name,
                        Enabled = true
                    };
                    uow.GameServers.Add(server);
                }
                if (server.Enabled)
                {
                    server.Host = _appSettings.GameServer.Network.Host;
                    server.Port = _appSettings.GameServer.Network.Port;
                    server.ShowName = _appSettings.GameServer.ShowName;
                    server.Type = _appSettings.GameServer.Type;
                    server.Heartbeat = DateTime.UtcNow;
                }
                await uow.CommitAsync();
            }
            return server.Enabled;
        }

        private async Task<bool> Heartbeat()
        {
            GameServer server = null;
            await using (var uow = await _unitOfWorkFactory.Create())
            {
                server = await uow.GameServers.Where(p => p.Name == _appSettings.GameServer.Name).FirstOrDefaultAsync();
                if (server == null) return false;
                server.Heartbeat = DateTime.UtcNow;
                await uow.CommitAsync();
            }
            return server.Enabled;
        }
    }
}
