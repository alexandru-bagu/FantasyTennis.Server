using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FTServer.Contracts.Services.Database;
using FFTServer.Database.Seed.Services;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseDatabaseSeed(this IHostBuilder hostBuilder) 
    {
        return hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddSingleton<IDataSeedService, DataSeedService>();
        });
    }
}
