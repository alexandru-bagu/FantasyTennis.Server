using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface IShopItemDataStore : IReadOnlyDictionary<int, ShopItem>
    {

    }
}
