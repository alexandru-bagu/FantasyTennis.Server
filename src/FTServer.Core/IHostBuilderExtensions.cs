using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FTServer.Contracts.Services.Database;
using FTServer.Core.Services.Database;
using FTServer.Security;
using FTServer.Contracts.Security;
using FTServer.Core.Settings;
using FTServer.Contracts.Services.Network;
using FTServer.Core.Services.Network;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseFantasyTennis(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("settings.core.json");
        });
        return hostBuilder.ConfigureServices((context, services) =>
        {
            services.Configure<CoreSettings>(context.Configuration);

            services.AddSingleton<IDataSeedService, DataSeedService>();
            services.AddSingleton<ISecureHashProvider, SecureHashProvider>();

            services.AddTransient<INetworkServiceFactory, NetworkServiceFactory>();
        });
    }
}
