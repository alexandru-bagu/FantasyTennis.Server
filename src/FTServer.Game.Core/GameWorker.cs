using System;
using System.Threading;
using System.Threading.Tasks;
using FTServer.Database.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FTServer.Game.Core
{
    public class GameWorker : BackgroundService
    {
        private readonly ILogger<GameWorker> _logger;

        public GameWorker(ILogger<GameWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
