using FTServer.Contracts.Game;
using FTServer.Contracts.MemoryManagement;
using FTServer.Core.Services.Game;
using FTServer.Core.Services.MemoryManagement;
using FTServer.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseCore(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureAppConfiguration((context, builder) =>
            {
                if (File.Exists("settings.core.json"))
                    builder.AddJsonFile("settings.core.json");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<CoreSettings>(context.Configuration);
                services.AddSingleton<ICharacterStatValidationService, CharacterStatValidationService>();
                services.AddSingleton<ICharacterEquipmentBuilder, CharacterEquipmentBuilder>();
                services.AddSingleton<IUnmanagedMemoryService, UnmanagedMemoryService>();
            });
    }
}
