using FTServer.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace FTServer.Database.MySql
{
    public class MySqlDbContext : CoreDbContext
    {
        public MySqlDbContext()
        {
        }

        public MySqlDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
