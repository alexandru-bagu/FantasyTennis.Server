using FTServer.Contracts.MemoryManagement;
using FTServer.Core.Services.MemoryManagement;
using FTServer.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseCore(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("settings.core.json");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<CoreSettings>(context.Configuration);

                services.AddSingleton<IUnmanagedMemoryService, UnmanagedMemoryService>();
            });
    }
}
