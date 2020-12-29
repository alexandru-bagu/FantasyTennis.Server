using FTServer.Contracts.Database;
using FTServer.Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FTServer.Database.Seed.DebugSeed
{
    public class CharacterSeed : IDataSeeder
    {
        public async Task SeedAsync(IUnitOfWork uow)
        {
#if DEBUG
            var account = await uow.Accounts.FirstAsync();
            var character = new Character();
            character.AccountId = account.Id;
            character.Name = "test";
            character.Level = 1;
            character.Strength =
                character.Stamina =
                character.Dexterity =
                character.Willpower = 15;
            character.StatusPoints = 5;
            character.Enabled = true;
            character.Gold = 100000;
            character.Type = HeroType.Niki;
            character.IsCreated = true;
            uow.Characters.Add(character);
#endif
        }
    }
}
