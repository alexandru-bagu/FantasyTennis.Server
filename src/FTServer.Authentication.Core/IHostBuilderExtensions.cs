using FTServer.Authentication.Core;
using FTServer.Authentication.Core.Services;
using FTServer.Authentication.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseAuthenticationServer(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .UseCore()
            .UseNetwork<AuthenticationNetworkContext>()
            .ConfigureAppConfiguration((context, builder) =>
            {
                builder.AddJsonFile("settings.auth.json");
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<AppSettings>(context.Configuration);
                services.AddSingleton<IAuthenticationSynchronizationContextService, AuthenticationSynchronizationContextService>();
                services.AddHostedService<AuthenticationKernel>();
            });
    }
}
