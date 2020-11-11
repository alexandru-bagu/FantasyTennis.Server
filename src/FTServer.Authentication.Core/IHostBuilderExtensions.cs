using FTServer.Authentication.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseAuthenticationServer(this IHostBuilder hostBuilder)
    {
        hostBuilder.UseFantasyTennis();
        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("auth.settings.json");
        });
        return hostBuilder.ConfigureServices(services =>
        {
            services.AddHostedService<AuthenticationWorker>();
        });
    }
}
