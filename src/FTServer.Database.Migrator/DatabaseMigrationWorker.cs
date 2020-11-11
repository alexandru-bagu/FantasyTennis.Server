using System.Threading;
using System.Threading.Tasks;
using FTServer.Database.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FTServer.Database.Migrator
{
    public class DatabaseMigrationWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public DatabaseMigrationWorker(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime hostApplicationLifetime)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<IDbContext>();
                await dbContext.Database.MigrateAsync();
                _hostApplicationLifetime.StopApplication();
            }
        }
    }
}
