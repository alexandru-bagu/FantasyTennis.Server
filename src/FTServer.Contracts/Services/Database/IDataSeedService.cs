using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Database
{
    public interface IDataSeedService
    {
        Task SeedAsync();
    }
}
