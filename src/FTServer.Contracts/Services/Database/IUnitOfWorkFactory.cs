using FTServer.Contracts.Database;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Database
{
    public interface IUnitOfWorkFactory
    {
        Task<IUnitOfWork> Create();
    }
}
