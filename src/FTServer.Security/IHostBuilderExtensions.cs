using FTServer.Contracts.Security;
using FTServer.Security;
using FTServer.Security.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseSecurity(this IHostBuilder hostBuilder)
    {
        return hostBuilder
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ISecureHashProvider, SecureHashProvider>();
                services.AddSingleton<IMessageChecksumService, MessageChecksumService>();
                services.AddSingleton<ICryptographyServiceFactory, CryptographyServiceFactory>();

                services.AddTransient<IMessageSerialService, MessageSerialService>();
            });
    }
}
