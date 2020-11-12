using System;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Services.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FTServer.Game.Core
{
    public class GameKernel : BackgroundService
    {
        private readonly ILogger<GameKernel> _logger;
        private readonly IDataSeedService _dataSeedService;

        public GameKernel(ILogger<GameKernel> logger, IDataSeedService dataSeedService)
        {
            _logger = logger;
            _dataSeedService = dataSeedService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _dataSeedService.SeedAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
