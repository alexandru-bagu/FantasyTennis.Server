using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Database;
using FTServer.Contracts.Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FTServer.Database.Migrator
{
    public class DatabaseMigrationWorker : BackgroundService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDataSeedService _dataSeedService;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public DatabaseMigrationWorker(IUnitOfWorkFactory unitOfWorkFactory, IServiceScopeFactory serviceScopeFactory,  IDataSeedService dataSeedService, IHostApplicationLifetime hostApplicationLifetime)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _serviceScopeFactory = serviceScopeFactory;
            _dataSeedService = dataSeedService;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var databaseContext = scope.ServiceProvider.GetService<IRawDbContext>();
                await databaseContext.Database.MigrateAsync();
            }
            await _dataSeedService.SeedAsync();
            _hostApplicationLifetime.StopApplication();
        }
    }
}
