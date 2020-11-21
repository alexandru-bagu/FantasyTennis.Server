using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading.Tasks;

namespace FTServer.Contracts.Database
{
    public interface IRawDbContext : IDbContext
    {
        DatabaseFacade Database { get; }
        Task SaveChangesAsync();
        void SaveChanges();
    }
}
