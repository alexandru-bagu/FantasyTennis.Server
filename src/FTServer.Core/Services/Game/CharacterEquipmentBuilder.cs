using FTServer.Contracts.Game;
using FTServer.Contracts.Stores;
using FTServer.Database.Model;
using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Core.Services.Game
{
    public class CharacterEquipmentBuilder : ICharacterEquipmentBuilder
    {
        private readonly IShopItemDataStore _shopItemDataStore;
        private readonly IItemPartDataStore _itemPartDataStore;

        public CharacterEquipmentBuilder(IShopItemDataStore shopItemDataStore, IItemPartDataStore itemPartDataStore)
        {
            _shopItemDataStore = shopItemDataStore;
            _itemPartDataStore = itemPartDataStore;
        }

        public Equipment Generate(IEnumerable<Item> items)
        {
            var eq = new Equipment();
            foreach (var item in items)
            {
                if (_shopItemDataStore.TryGetValue(item.Index, out ShopItem shopItem))
                {
                    //do something
                }
            }
            return eq;
        }
    }
}
