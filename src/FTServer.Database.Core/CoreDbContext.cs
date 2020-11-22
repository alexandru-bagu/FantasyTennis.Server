using FTServer.Contracts.Database;
using FTServer.Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace FTServer.Database.Core
{
    public abstract class CoreDbContext : DbContext, IRawDbContext
    {
        public DbSet<DataSeed> DataSeeds { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<GameServer> GameServers { get; set; }
        public DbSet<RelayServer> RelayServers { get; set; }

        DatabaseFacade IRawDbContext.Database => Database;

        protected CoreDbContext()
        {
        }

        protected CoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameServer>()
                .HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<RelayServer>()
                .HasIndex(p => p.Name).IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        void IRawDbContext.SaveChanges()
        {
            SaveChanges();
        }

        async Task IRawDbContext.SaveChangesAsync()
        {
            await SaveChangesAsync();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
        }

        void IDbContext.Attach<T>(T entity)
        {
            base.Attach(entity);
        }

        public void Detach<T>(T entity)
        {
            base.Remove(entity);
        }
    }
}
