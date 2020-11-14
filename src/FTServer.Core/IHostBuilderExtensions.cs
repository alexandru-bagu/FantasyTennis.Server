using FTServer.Contracts.MemoryManagement;
using FTServer.Contracts.Security;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Services.Network;
using FTServer.Core.Services.Database;
using FTServer.Core.Services.MemoryManagement;
using FTServer.Core.Services.Network;
using FTServer.Core.Services.Security;
using FTServer.Core.Settings;
using FTServer.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddSingleton<IUnmanagedMemoryService, UnmanagedMemoryService>();
            services.AddSingleton<ICryptographicServiceFactory, CryptographicServiceFactory>();

            services.AddTransient<INetworkServiceFactory, NetworkServiceFactory>();
        });
    }
}
