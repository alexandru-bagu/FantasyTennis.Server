using FTServer.Contracts.Database;

namespace FTServer.Contracts.Services.Database
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create();
    }
}
