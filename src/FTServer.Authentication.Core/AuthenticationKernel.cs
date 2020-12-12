using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Authentication.Core.Network;
using FTServer.Authentication.Core.Settings;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Services.Network;
using FTServer.Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Authentication.Core
{
    public class AuthenticationKernel : BackgroundService
    {
        private readonly ILogger<AuthenticationKernel> _logger;
        private readonly INetworkServiceFactory _networkServiceFactory;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AppSettings _appSettings;
        private INetworkService<AuthenticationNetworkContext> _authenticationNetworkService;
        
        public AuthenticationKernel(ILogger<AuthenticationKernel> logger,
            IServiceProvider serviceProvider,
            INetworkServiceFactory networkServiceFactory,
            IOptions<AppSettings> appSettings,
            INetworkMessageHandlerService<AuthenticationNetworkContext> networkMessageHandlerService,
            IUnitOfWorkFactory unitOfWorkFactory)
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
                await using (var uow = _unitOfWorkFactory.Create())
                {
                    (await uow.Accounts.Where(p => p.Online).Select(p => new Account() { Id = p.Id, Online = p.Online }).ToListAsync())
                        .ForEach(p => { uow.Attach(p); p.Online = false; });
                    await uow.CommitAsync();
                }

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
