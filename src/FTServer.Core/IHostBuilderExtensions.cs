using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseFantasyTennis(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("core.settings.json");
        });
        return hostBuilder.ConfigureServices((context, services) =>
        {

        });
    }
}
