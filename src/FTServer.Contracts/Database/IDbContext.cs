using FTServer.Database.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace FTServer.Contracts.Database
{
    public interface IDbContext : IDisposable, IAsyncDisposable
    {
        DbSet<DataSeed> DataSeeds { get; }
        DbSet<Account> Accounts { get; }
        DbSet<Login> Logins { get; }
        DbSet<LoginAttempt> LoginAttempts { get; }
        DbSet<Character> Characters { get; }
        DbSet<Item> Items { get; }
        DbSet<Home> Homes { get; }
        DbSet<Furniture> Furniture { get; }
        DbSet<GameServer> GameServers { get; }
        DbSet<RelayServer> RelayServers { get; }
        DbSet<Friendship> Friendships { get; }
        DbSet<TutorialProgress> TutorialProgress { get; }
        DbSet<ChallengeProgress> ChallengeProgress { get; }

        void Attach<T>(T entity);
        void Detach<T>(T entity);
    }
}
