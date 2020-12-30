using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Services.Network;
using FTServer.Database.Model;
using FTServer.Relay.Core.Network;
using FTServer.Relay.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Relay.Core
{
    public class RelayKernel : BackgroundService
    {
        private readonly ILogger<RelayKernel> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AppSettings _appSettings;

        public RelayKernel(ILogger<RelayKernel> logger, 
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
                _logger.LogInformation("Starting relay server.");
                if (await IsServerEnabled())
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var networkMessageHandlerService = scope.ServiceProvider.GetService<INetworkMessageHandlerService<RelayNetworkContext>>();
                        networkMessageHandlerService.RegisterDefaultHandler(scope.ServiceProvider.Create<DefaultNetworkMessageHandler>());
                        var networkServiceFactory = scope.ServiceProvider.GetService<INetworkServiceFactory>();

                        using (var networkService = networkServiceFactory.Create<RelayNetworkContext>(_appSettings.RelayServer.Network.Host, _appSettings.RelayServer.Network.Port))
                        {
                            await networkService.ListenAsync();

                            _logger.LogInformation("Relay server started.");

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
            RelayServer server = null;
            await using (var uow = _unitOfWorkFactory.Create())
            {
                server = await uow.RelayServers.Where(p => p.Name == _appSettings.RelayServer.Name).FirstOrDefaultAsync();
                if (server == null)
                {
                    server = new RelayServer()
                    {
                        Name = _appSettings.RelayServer.Name,
                        Enabled = true
                    };
                    uow.RelayServers.Add(server);
                }
                if (server.Enabled)
                {
                    server.Host = _appSettings.RelayServer.Network.Host;
                    server.Port = _appSettings.RelayServer.Network.Port;
                    server.Heartbeat = DateTime.UtcNow;
                }
                await uow.CommitAsync();
            }
            return server.Enabled;
        }

        private async Task<bool> Heartbeat()
        {
            RelayServer server = null;
            await using (var uow = _unitOfWorkFactory.Create())
            {
                server = await uow.RelayServers.Where(p => p.Name == _appSettings.RelayServer.Name).FirstOrDefaultAsync();
                if (server == null) return false;
                server.Heartbeat = DateTime.UtcNow;
                await uow.CommitAsync();
            }
            return server.Enabled;
        }
    }
}
