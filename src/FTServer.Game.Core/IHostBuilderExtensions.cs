using FTServer.Game.Core;
using FTServer.Game.Core.Services;
using FTServer.Game.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseGameServer(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseCore()
            .UseResources()
            .UseNetwork<GameNetworkContext>()
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("settings.game.json");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<AppSettings>(context.Configuration);
                services.AddSingleton<IConcurrentUserTrackingService, ConcurrentUserTrackingService>();
                services.AddHostedService<GameKernel>();
            });
    }
}
