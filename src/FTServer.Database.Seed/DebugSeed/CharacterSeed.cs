using FTServer.Contracts.Database;
using FTServer.Contracts.Game;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FTServer.Database.Seed.DebugSeed
{
    public class CharacterSeed : IDataSeeder
    {
        private readonly ICharacterBuilder _characterBuilder;

        public CharacterSeed(ICharacterBuilder characterBuilder)
        {
            _characterBuilder = characterBuilder;
        }

        public async Task SeedAsync(IUnitOfWork uow)
        {
#if DEBUG
            var account = await uow.Accounts.FirstAsync();
            var character = await _characterBuilder.Create(uow, (character) =>
            {
                character.AccountId = account.Id;
                character.Gold = 100000;
                character.Type = HeroType.Niki;
                return Task.FromResult(character);
            });
#endif
        }
    }
}
