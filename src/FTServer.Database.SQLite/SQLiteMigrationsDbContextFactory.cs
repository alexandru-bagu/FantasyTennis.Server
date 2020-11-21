using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FTServer.Database.SQLite
{
    public class SQLiteMigrationsDbContextFactory : IDesignTimeDbContextFactory<SQLiteDbContext>
    {
        public SQLiteDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var settings = configuration.ReadSQLiteSettings();

            var builder = new DbContextOptionsBuilder<SQLiteDbContext>()
                .UseSqlite($"Data Source=\"{settings.Path}\"");

            return new SQLiteDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.sqlite.json", optional: false);

            return builder.Build();
        }
    }
}
