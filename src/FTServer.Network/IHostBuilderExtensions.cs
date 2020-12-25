using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Network;
using FTServer.Network.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseNetwork<TNetworkContext>(this IHostBuilder hostBuilder)
        where TNetworkContext : INetworkContext
    {
        return hostBuilder
             .UseCore()
             .UseSecurity()
             .ConfigureServices((context, services) =>
             {
                 services.AddTransient<INetworkMessageFactory, NetworkMessageFactory>();

                 services.AddScoped<INetworkServiceFactory, NetworkServiceFactory>();
                 services.AddScoped<INetworkMessageHandlerService<TNetworkContext>, NetworkMessageHandlerService<TNetworkContext>>();
             });
    }
}
