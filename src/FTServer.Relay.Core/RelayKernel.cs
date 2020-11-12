using System;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Contracts.Services.Database;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FTServer.Relay.Core
{
    public class RelayKernel : BackgroundService
    {
        private readonly ILogger<RelayKernel> _logger;
        private readonly IDataSeedService _dataSeedService;

        public RelayKernel(ILogger<RelayKernel> logger, IDataSeedService dataSeedService)
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
