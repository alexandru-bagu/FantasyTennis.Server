using System;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Services.Network;
using FTServer.Game.Core.Network;
using FTServer.Game.Core.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Game.Core
{
    public class GameKernel : BackgroundService
    {
        private readonly ILogger<GameKernel> _logger;
        private readonly INetworkServiceFactory _networkServiceFactory;
        private readonly AppSettings _appSettings;
        private INetworkService<GameNetworkContext> _gameNetworkService;

        public GameKernel(ILogger<GameKernel> logger, IServiceProvider serviceProvider, INetworkServiceFactory networkServiceFactory, IOptions<AppSettings> appSettings, INetworkMessageHandlerService<GameNetworkContext> networkMessageHandlerService)
        {
            _logger = logger;
            _networkServiceFactory = networkServiceFactory;
            _appSettings = appSettings.Value;
            networkMessageHandlerService.RegisterDefaultHandler(serviceProvider.Create<DefaultNetworkMessageHandler>());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                using (_gameNetworkService = _networkServiceFactory.Create<GameNetworkContext>(_appSettings.GameServer.Network.Host, _appSettings.GameServer.Network.Port))
                {
                    await _gameNetworkService.ListenAsync();

                    _logger.LogInformation("Game server started.");
                    await Task.Delay(-1, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatal: {ex.Message}");
            }
        }
    }
}
