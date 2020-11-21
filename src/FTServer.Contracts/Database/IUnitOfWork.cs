using System;
using System.Threading.Tasks;

namespace FTServer.Contracts.Database
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IDbContext DatabaseContext { get; }
        Task CommitAsync();
        Task AbortAsync();
    }
}
