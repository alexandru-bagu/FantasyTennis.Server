using System;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Authentication.Core.Settings;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Services.Network;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Authentication.Core
{
    public class AuthenticationKernel : BackgroundService
    {
        private readonly ILogger<AuthenticationKernel> _logger;
        private readonly IDataSeedService _dataSeedService;
        private readonly INetworkServiceFactory _networkServiceFactory;
        private readonly AppSettings _appSettings;
        private INetworkService<AuthenticationNetworkContext> _authenticationNetworkService;

        public AuthenticationKernel(ILogger<AuthenticationKernel> logger, IDataSeedService dataSeedService, INetworkServiceFactory networkServiceFactory, IOptions<AppSettings> appSettings)
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
                using (_authenticationNetworkService = _networkServiceFactory.Create<AuthenticationNetworkContext>(_appSettings.AuthServer.Network.Host, _appSettings.AuthServer.Network.Port))
                {
                    await _authenticationNetworkService.ListenAsync();

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
