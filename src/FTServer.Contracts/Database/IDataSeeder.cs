using System.Threading.Tasks;

namespace FTServer.Contracts.Database
{
    /// <summary>
    /// Interface initialized by IDataSeedService; Use [DependsOn()] to define the order of seeding
    /// </summary>
    public interface IDataSeeder
    {
        Task SeedAsync(IUnitOfWork uow);
    }
}
