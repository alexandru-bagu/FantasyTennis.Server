using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Hero;
using FTServer.Contracts.Stores.Pet;
using FTServer.Resources.Services;
using FTServer.Resources.Stores.Hero;
using FTServer.Resources.Stores.Pet;
using FTServer.Resources.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FTServer.Contracts.Stores;
using System.IO;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseResources(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureAppConfiguration((context, builder) =>
            {
                if(File.Exists("settings.resources.json"))
                    builder.AddJsonFile("settings.resources.json");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<AppSettings>(context.Configuration);
                services.AddSingleton<IResourceManager, GameResourceManager>();
                services.AddSingleton<IResourceCryptographyService, GameResourceCryptographyService>();

                services.AddSingleton<IHeroLevelDataStore, HeroLevelDataStore>();
                services.AddSingleton<IPetLevelDataStore, PetLevelDataStore>();
                services.AddSingleton<IItemDataStore, ItemDataStore>();
            });
    }
}
