using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using FTServer.Database.SQLite;
using System.IO;

public static class IHostBuilderExtensions
{
    internal static SQLiteSettings ReadSQLiteSettings(this IConfiguration configuration)
    {
        var settingsObj = new { SQLiteSettings = new SQLiteSettings() };
        configuration.Bind(settingsObj);

        var settings = settingsObj.SQLiteSettings;
        if (settings.Path.StartsWith("~/"))
        {
            settings.Path = settings.Path.Replace("~/", "");
            settings.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), settings.Path);
        }
        var dir = Path.GetDirectoryName(settings.Path);
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        return settings;
    }

    public static IHostBuilder UseSQLiteDatabase(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureAppConfiguration((context, builder) =>
        {
            builder.AddJsonFile("settings.sqlite.json");
        });

        return hostBuilder.UseDatabase<SQLiteDbContext>((context, builder) =>
        {
            var settings = context.Configuration.ReadSQLiteSettings();
            builder.UseSqlite($"Data Source=\"{settings.Path}\"");
        });
    }
}
