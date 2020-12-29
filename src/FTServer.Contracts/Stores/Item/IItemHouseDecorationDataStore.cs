using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores.Item
{
    public interface IItemHouseDecorationDataStore : IReadOnlyDictionary<int, ItemHouseDecoration>
    {
        HashSet<int> ByKind(HouseDecorationKind kind);
    }
}
