using FTServer.Database.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Threading.Tasks;

namespace FTServer.Database.Core
{
    public interface IDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Login> Logins { get; set; }
        DbSet<LoginAttempt> LoginAttempts { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Home> Homes { get; set; }
        DbSet<Furniture> Furniture { get; set; }
        DbSet<GameServer> GameServers { get; set; }
        DbSet<RelayServer> RelayServers { get; set; }

        DatabaseFacade Database { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
