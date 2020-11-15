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
                 services.AddTransient<INetworkServiceFactory, NetworkServiceFactory>();
                 services.AddTransient<INetworkMessageFactory, NetworkMessageFactory>();
             })
             .ConfigureServices((context, services) =>
             {
                 services.AddSingleton<INetworkMessageHandlerService<TNetworkContext>, NetworkMessageHandlerService<TNetworkContext>>();
             });
    }
}
