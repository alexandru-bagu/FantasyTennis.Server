using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Database;
using FTServer.Contracts.Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FTServer.Database.Migrator
{
    public class DatabaseMigrationWorker : BackgroundService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IDataSeedService _dataSeedService;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public DatabaseMigrationWorker(IUnitOfWorkFactory unitOfWorkFactory, IDataSeedService dataSeedService, IHostApplicationLifetime hostApplicationLifetime)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _dataSeedService = dataSeedService;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await using (var unitOfWork = await _unitOfWorkFactory.Create())
            {
                var databaseContext = unitOfWork.DatabaseContext as IRawDbContext;
                await databaseContext.Database.MigrateAsync();
                await unitOfWork.CommitAsync();
            }
            await _dataSeedService.SeedAsync();
            _hostApplicationLifetime.StopApplication();
        }
    }
}
