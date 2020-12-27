using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface IItemEnchantDataStore : IReadOnlyDictionary<int, ItemEnchant>
    {
        HashSet<int> ByKind(EnchantKind kind);
    }
}
