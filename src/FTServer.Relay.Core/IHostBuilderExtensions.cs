using FTServer.Relay.Core;
using FTServer.Relay.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseRelayServer(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseCore()
            .UseNetwork<RelayNetworkContext>()
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("settings.relay.json");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<AppSettings>(context.Configuration);
                services.AddHostedService<RelayKernel>();
            });
    }
}
