using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using FTServer.Database.MySql;
using Microsoft.EntityFrameworkCore;

public static class IHostBuilderExtensions
{
    internal static MySqlSettings ReadMySqlSettings(this IConfiguration configuration)
    {
        var settings = new { MySqlSettings = new MySqlSettings() };
        configuration.Bind(settings);
        return settings.MySqlSettings;
    }

    public static IHostBuilder UseMySqlDatabase(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("settings.mysql.json");
        });

        return hostBuilder.UseDatabase<MySqlDbContext>((context, builder) =>
        {
            var settings = context.Configuration.ReadMySqlSettings();
            builder.UseMySql(settings.ConnectionString, ServerVersion.AutoDetect(settings.ConnectionString));
        });
    }
}
