using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FTServer.Database.MySql
{
    public class MySqlMigrationsDbContextFactory : IDesignTimeDbContextFactory<MySqlDbContext>
    {
        public MySqlDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var settings = configuration.ReadMySqlSettings();

            var builder = new DbContextOptionsBuilder<MySqlDbContext>()
                .UseMySql(settings.ConnectionString, ServerVersion.AutoDetect(settings.ConnectionString));

            return new MySqlDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("settings.mysql.json", optional: false);

            return builder.Build();
        }
    }
}
