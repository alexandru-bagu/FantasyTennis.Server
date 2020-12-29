using FTServer.Contracts;
using FTServer.Contracts.Database;
using FTServer.Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FTServer.Database.Seed.DebugSeed
{
    [DependsOn(typeof(CharacterSeed))]
    public class HomeSeed : IDataSeeder
    {
        public async Task SeedAsync(IUnitOfWork uow)
        {
#if DEBUG
            var character = await uow.Characters.FirstAsync();
            var home = new Home();
            home.CharacterId = character.Id;
            home.Level = 0;
            uow.Homes.Add(home);
#endif
        }
    }
}
