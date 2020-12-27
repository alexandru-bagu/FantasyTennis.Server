using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface IItemHouseDecorationDataStore : IReadOnlyDictionary<int, ItemHouseDecoration>
    {
        HashSet<int> ByKind(HouseDecorationKind kind);
    }
}
