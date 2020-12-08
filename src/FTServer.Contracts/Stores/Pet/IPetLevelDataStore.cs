using System.Collections.Generic;

namespace FTServer.Contracts.Stores.Pet
{
    public interface IPetLevelDataStore : IReadOnlyDictionary<byte, int>
    {
    }
}
