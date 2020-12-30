using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Services.Network;
using FTServer.Contracts.Stores;
using FTServer.Database.Model;
using FTServer.Game.Core.Network;
using FTServer.Game.Core.Services;
using FTServer.Game.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Game.Core
{
    public class GameKernel : BackgroundService
    {
        private readonly ILogger<GameKernel> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AppSettings _appSettings;

        public GameKernel(ILogger<GameKernel> logger,
            IServiceScopeFactory serviceScopeFactory, 
            IOptions<AppSettings> appSettings,
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _appSettings = appSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Starting game server.");
                if (await IsServerEnabled())
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var networkMessageHandlerService = scope.ServiceProvider.GetService<INetworkMessageHandlerService<GameNetworkContext>>();
                        networkMessageHandlerService.RegisterDefaultHandler(scope.ServiceProvider.Create<DefaultNetworkMessageHandler>());
                        var networkServiceFactory = scope.ServiceProvider.GetService<INetworkServiceFactory>();
                        
                        var currentServer = scope.ServiceProvider.GetService<CurrentServer>();
                        await currentServer.Initialize();

                        var trackingService = scope.ServiceProvider.GetService<IConcurrentUserTrackingService>();
                        await trackingService.Reset();

                        using (var networkService = networkServiceFactory.Create<GameNetworkContext>(_appSettings.GameServer.Network.Host, _appSettings.GameServer.Network.Port))
                        {
                            await networkService.ListenAsync();

                            _logger.LogInformation("Game server started.");

                            while (!stoppingToken.IsCancellationRequested)
                            {
                                await Heartbeat();
                                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                            }
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
            await using (var uow = _unitOfWorkFactory.Create())
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
            await using (var uow = _unitOfWorkFactory.Create())
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
