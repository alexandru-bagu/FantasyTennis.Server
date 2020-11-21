using FTServer.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace FTServer.Contracts.Database
{
    public interface IDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<DataSeed> DataSeeds { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Login> Logins { get; set; }
        DbSet<LoginAttempt> LoginAttempts { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Home> Homes { get; set; }
        DbSet<Furniture> Furniture { get; set; }
        DbSet<GameServer> GameServers { get; set; }
        DbSet<RelayServer> RelayServers { get; set; }
    }
}
