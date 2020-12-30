using FTServer.Contracts.Database;
using FTServer.Contracts.Game;
using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using System;
using System.Threading.Tasks;

namespace FTServer.Core.Services.Game
{
    public class CharacterBuilder : ICharacterBuilder
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CharacterBuilder(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Character> Create(IUnitOfWork uow, Func<Character, Task<Character>> builder)
        {
            var character = new Character();
            character.Enabled = true;
            character.IsCreated = false;
            character.NameChangeAllowed = false;
            character.Level = 1;
            await builder(character);
            uow.Characters.Add(character);
            await uow.CommitAsync();
            return character;
        }

        public async Task<Character> Create(Func<Character, Task<Character>> builder)
        {
            await using (var uow = _unitOfWorkFactory.Create())
                return await Create(uow, builder);
        }

        public Task<Character> Create(int accountId, HeroType heroType)
        {
            return Create((character) =>
            {
                character.AccountId = accountId;
                character.Type = heroType;
                return Task.FromResult(character);
            });
        }
    }
}
