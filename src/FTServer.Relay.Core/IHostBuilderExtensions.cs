using FTServer.Relay.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseRelayServer(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseFantasyTennis();
        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("relay.settings.json");
        });
        return hostBuilder.ConfigureServices(services =>
        {
            services.AddHostedService<RelayWorker>();
        });
    }
}
