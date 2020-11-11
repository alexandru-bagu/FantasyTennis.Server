using FTServer.Database.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace FTServer.Database.Core
{
    public abstract class CoreDbContext : DbContext, IDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<LoginAttempt> LoginAttempts { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Home> Homes { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<GameServer> GameServers { get; set; }
        public DbSet<RelayServer> RelayServers { get; set; }

        DatabaseFacade IDbContext.Database => Database;

        private IDbContextTransaction _transactions;

        protected CoreDbContext()
        {
            _transactions = null;
        }

        protected CoreDbContext(DbContextOptions options) : base(options)
        {
            _transactions = null;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameServer>()
                .HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<RelayServer>()
                .HasIndex(p => p.Name).IsUnique();
            base.OnModelCreating(modelBuilder);
        }

        public async Task BeginTransactionAsync()
        {
            if (_transactions == null)
            {
                _transactions = await Database.BeginTransactionAsync();
            }
            else
            {
                throw new Exception("A transaction is already in progress.");
            }
        }

        public async Task CommitAsync()
        {
            if (_transactions != null)
            {
                await SaveChangesAsync();
                await _transactions.CommitAsync();
                await _transactions.DisposeAsync();
                _transactions = null;
            }
            else
            {
                throw new Exception("No transaction is in progress.");
            }
        }

        public async Task RollbackAsync()
        {
            if (_transactions != null)
            {
                await _transactions.RollbackAsync();
                await _transactions.DisposeAsync();
                _transactions = null;
            }
            else
            {
                throw new Exception("No transaction is in progress.");
            }
        }

        private void Rollback()
        {
            if (_transactions != null)
            {
                _transactions.Rollback();
                _transactions.Dispose();
                _transactions = null;
            }
        }

        public override void Dispose()
        {
            Rollback();
            base.Dispose();
        }

        public override async ValueTask DisposeAsync()
        {
            if (_transactions != null)
                await RollbackAsync();
            await base.DisposeAsync();
        }
    }
}
