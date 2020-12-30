using FTServer.Contracts.Database;
using FTServer.Database.Model;
using System;
using System.Threading.Tasks;

namespace FTServer.Contracts.Game
{
    public interface ICharacterBuilder
    {
        Task<Character> Create(IUnitOfWork uow, Func<Character, Task<Character>> builder);
        Task<Character> Create(Func<Character, Task<Character>> builder);
        Task<Character> Create(int accountId, HeroType heroType);
    }
}
