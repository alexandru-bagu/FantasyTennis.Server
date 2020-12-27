using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface IShopItemDataStore : IReadOnlyDictionary<int, ShopItem>
    {
        IEnumerable<ShopItem> Search(int categoryType, int partType, int heroType);
    }
}
