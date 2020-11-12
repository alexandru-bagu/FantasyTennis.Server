using FTServer.Authentication.Core;
using FTServer.Authentication.Core.Settings;
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
            builder.AddJsonFile("settings.auth.json");
        });
        return hostBuilder.ConfigureServices((context, services) =>
        {
            services.Configure<AppSettings>(context.Configuration);
            services.AddHostedService<AuthenticationKernel>();
        });
    }
}
