using FTServer.Game.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseGameServer(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseFantasyTennis();
        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("game.settings.json");
        });
        return hostBuilder.ConfigureServices(services =>
        {
            services.AddHostedService<GameWorker>();
        });
    }
}
