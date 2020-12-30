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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FTServer.Authentication.Core
{
    public class AuthenticationKernel : BackgroundService
    {
        private readonly ILogger<AuthenticationKernel> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AppSettings _appSettings;

        public AuthenticationKernel(ILogger<AuthenticationKernel> logger,
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
                _logger.LogInformation("Starting authentication server.");
                await ResetOnlinePlayers();
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var networkMessageHandlerService = scope.ServiceProvider.GetService<INetworkMessageHandlerService<AuthenticationNetworkContext>>();
                    networkMessageHandlerService.RegisterDefaultHandler(scope.ServiceProvider.Create<DefaultNetworkMessageHandler>());
                    var networkServiceFactory = scope.ServiceProvider.GetService<INetworkServiceFactory>();

                    using (var networkService = networkServiceFactory.Create<AuthenticationNetworkContext>(_appSettings.AuthServer.Network.Host, _appSettings.AuthServer.Network.Port))
                    {
                        await networkService.ListenAsync();

                        _logger.LogInformation("Authentication server started.");
                        await Task.Delay(-1, stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fatal: {ex.Message}");
            }
        }

        private async Task ResetOnlinePlayers()
        {
            await using (var uow = _unitOfWorkFactory.Create())
            {
                (await uow.Accounts.Where(p => p.Online).Select(p => new Account() { Id = p.Id, Online = p.Online }).ToListAsync())
                    .ForEach(p => { uow.Attach(p); p.Online = false; });
                await uow.CommitAsync();
            }
        }
    }
}
