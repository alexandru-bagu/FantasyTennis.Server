using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FTServer.Database.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using FTServer.Contracts.Database;
using FTServer.Database.Core.Services;
using FTServer.Contracts.Services.Database;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseDatabase<T>(this IHostBuilder hostBuilder, Action<HostBuilderContext, DbContextOptionsBuilder> configure) where T : CoreDbContext
    {
        return hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddDbContext<T>(builder => configure(context, builder));
            var dbService = services.First(p => p.ServiceType == typeof(T));
            services.Add(new ServiceDescriptor(typeof(IDbContext), (serviceProvider) => serviceProvider.GetService<T>(), dbService.Lifetime));
            services.Add(new ServiceDescriptor(typeof(IRawDbContext), (serviceProvider) => serviceProvider.GetService<T>(), dbService.Lifetime));

            services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
            services.AddSingleton<IDataSeedService, DataSeedService>();
        });
    }
}
