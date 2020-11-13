using System;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Services.Network;
using FTServer.Game.Core.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Game.Core
{
    public class GameKernel : BackgroundService
    {
        private readonly ILogger<GameKernel> _logger;
        private readonly IDataSeedService _dataSeedService;
        private readonly INetworkServiceFactory _networkServiceFactory;
        private readonly AppSettings _appSettings;
        private INetworkService<GameNetworkContext> _gameNetworkService;

        public GameKernel(ILogger<GameKernel> logger, IDataSeedService dataSeedService, INetworkServiceFactory networkServiceFactory, IOptions<AppSettings> appSettings)
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
                using (_gameNetworkService = _networkServiceFactory.Create<GameNetworkContext>(_appSettings.GameServer.Network.Host, _appSettings.GameServer.Network.Port))
                {
                    await _gameNetworkService.ListenAsync();

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
