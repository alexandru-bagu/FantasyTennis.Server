using System.Collections.Generic;

namespace FTServer.Contracts.Stores.Hero
{
    public interface IHeroLevelDataStore : IReadOnlyDictionary<byte, int>
    {
    }
}
