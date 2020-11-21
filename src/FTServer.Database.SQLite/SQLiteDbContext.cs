using FTServer.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace FTServer.Database.SQLite
{
    public class SQLiteDbContext : CoreDbContext
    {
        public SQLiteDbContext()
        {
        }

        public SQLiteDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
