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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Relay.Core
{
    public class RelayKernel : BackgroundService
    {
        private readonly ILogger<RelayKernel> _logger;
        private readonly INetworkServiceFactory _networkServiceFactory;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AppSettings _appSettings;
        private INetworkService<RelayNetworkContext> _relayNetworkService;

        public RelayKernel(ILogger<RelayKernel> logger, IServiceProvider serviceProvider, 
            INetworkServiceFactory networkServiceFactory, IOptions<AppSettings> appSettings, 
            IUnitOfWorkFactory unitOfWorkFactory,
            INetworkMessageHandlerService<RelayNetworkContext> networkMessageHandlerService)
        {
            _logger = logger;
            _networkServiceFactory = networkServiceFactory;
            _unitOfWorkFactory = unitOfWorkFactory;
            _appSettings = appSettings.Value;

            networkMessageHandlerService.RegisterDefaultHandler(serviceProvider.Create<DefaultNetworkMessageHandler>());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                if (await IsServerEnabled())
                {
                    using (_relayNetworkService = _networkServiceFactory.Create<RelayNetworkContext>(_appSettings.RelayServer.Network.Host, _appSettings.RelayServer.Network.Port))
                    {
                        await _relayNetworkService.ListenAsync();

                        _logger.LogInformation("Relay server started.");

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
            RelayServer server = null;
            await using (var uow = await _unitOfWorkFactory.Create())
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
            await using (var uow = await _unitOfWorkFactory.Create())
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
