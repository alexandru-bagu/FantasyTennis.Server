using FTServer.Game.Core;
using FTServer.Game.Core.Settings;
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
            builder.AddJsonFile("settings.game.json");
        });
        return hostBuilder.ConfigureServices((context, services) =>
        {
            services.Configure<AppSettings>(context.Configuration);
            services.AddHostedService<GameKernel>();
        });
    }
}
