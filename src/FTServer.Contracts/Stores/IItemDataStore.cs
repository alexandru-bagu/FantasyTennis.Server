using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface IItemDataStore : IReadOnlyDictionary<int, ItemData>
    {

    }
}
