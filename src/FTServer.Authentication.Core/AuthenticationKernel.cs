using System;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Authentication.Core.Network;
using FTServer.Authentication.Core.Settings;
using FTServer.Contracts.Services.Network;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Authentication.Core
{
    public class AuthenticationKernel : BackgroundService
    {
        private readonly ILogger<AuthenticationKernel> _logger;
        private readonly INetworkServiceFactory _networkServiceFactory;
        private readonly AppSettings _appSettings;
        private INetworkService<AuthenticationNetworkContext> _authenticationNetworkService;

        public AuthenticationKernel(ILogger<AuthenticationKernel> logger, IServiceProvider serviceProvider, INetworkServiceFactory networkServiceFactory, IOptions<AppSettings> appSettings, INetworkMessageHandlerService<AuthenticationNetworkContext> networkMessageHandlerService)
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
                using (_authenticationNetworkService = _networkServiceFactory.Create<AuthenticationNetworkContext>(_appSettings.AuthServer.Network.Host, _appSettings.AuthServer.Network.Port))
                {
                    await _authenticationNetworkService.ListenAsync();

                    _logger.LogInformation("Authentication server started.");
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
