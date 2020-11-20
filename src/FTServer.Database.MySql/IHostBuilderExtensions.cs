using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using FTServer.Database.MySql;
using Microsoft.EntityFrameworkCore;

public static class IHostBuilderExtensions
{
    public static IHostBuilder UseMySqlDatabase(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("settings.mysql.json");
        });

        return hostBuilder.UseDatabase<MySqlDbContext>((context, builder) =>
        {
            var configuration = context.Configuration;
            var settings = new { MySqlSettings = new MySqlSettings() };
            configuration.Bind(settings);

            builder.UseMySql(settings.MySqlSettings.ConnectionString, ServerVersion.AutoDetect(settings.MySqlSettings.ConnectionString));
        });
    }
}
