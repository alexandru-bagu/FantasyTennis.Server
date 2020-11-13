using System;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Services.Network;
using FTServer.Relay.Core.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Relay.Core
{
    public class RelayKernel : BackgroundService
    {
        private readonly ILogger<RelayKernel> _logger;
        private readonly IDataSeedService _dataSeedService;
        private readonly INetworkServiceFactory _networkServiceFactory;
        private readonly AppSettings _appSettings;
        private INetworkService<RelayNetworkContext> _relayNetworkService;

        public RelayKernel(ILogger<RelayKernel> logger, IDataSeedService dataSeedService, INetworkServiceFactory networkServiceFactory, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _dataSeedService = dataSeedService;
            _networkServiceFactory = networkServiceFactory;
            _appSettings = appSettings.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await _dataSeedService.SeedAsync();
                using (_relayNetworkService = _networkServiceFactory.Create<RelayNetworkContext>(_appSettings.RelayServer.Network.Host, _appSettings.RelayServer.Network.Port))
                {
                    await _relayNetworkService.ListenAsync();

                    while (!stoppingToken.IsCancellationRequested)
                    {
                        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                        await Task.Delay(1000, stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatal: {ex.Message}");
            }
        }
    }
}
