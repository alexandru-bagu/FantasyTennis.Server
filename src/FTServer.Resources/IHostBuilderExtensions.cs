using FTServer.Contracts.Resources;
using FTServer.Resources.Services;
using FTServer.Resources.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseResources(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("settings.resources.json");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<AppSettings>(context.Configuration);
                services.AddSingleton<IResourceManager, GameResourceManager>();
                services.AddSingleton<IResourceCryptographyService, GameResourceCryptographyService>();
            });
    }
}
