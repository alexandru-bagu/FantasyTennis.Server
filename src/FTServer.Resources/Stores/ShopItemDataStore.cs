using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using FTServer.Contracts.Stores.Item;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Resources.Stores
{
    public class ShopItemDataStore : Dictionary<int, ShopItem>, IShopItemDataStore
    {
        private const string Resource = "Res/Script/Shop/Ini3/Shop_Ini3.xml";
        private readonly IItemPartDataStore _itemPartDataStore;
        private readonly IItemHouseDecorationDataStore _houseDecorationDataStore;
        private readonly IItemEnchantDataStore _enchantDataStore;
        private readonly IItemRecipeDataStore _recipeDataStore;

        public ShopItemDataStore(IResourceManager resourceManager,
            IItemPartDataStore itemPartDataStore,
            IItemHouseDecorationDataStore houseDecorationDataStore,
            IItemEnchantDataStore enchantDataStore,
            IItemRecipeDataStore recipeDataStore,
            ILogger<ShopItemDataStore> logger)
        {
            logger.LogInformation("loading...");
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            var tmp = new Dictionary<int, ShopItem>();
            foreach (dynamic itemRes in resource.ProductList)
            {
                var shopItem = new ShopItem();
                shopItem.Display = itemRes.DISPLAY;
                shopItem.Index = itemRes.Index;
                shopItem.Enable = itemRes.Enable == 1;
                shopItem.Free = itemRes.Free == 1;
                shopItem.Sale = itemRes.Sale == 1;
                shopItem.Event = itemRes.Event == 1;
                shopItem.Couple = itemRes.Couple == 1;
                shopItem.Nobuy = itemRes.Nobuy == 1;
                shopItem.Rand = itemRes.Rand != "No";
                shopItem.UseType = ItemUseType.Parse(itemRes.UseType);
                shopItem.Use0 = itemRes.Use0;
                shopItem.Use1 = itemRes.Use1;
                shopItem.Use2 = itemRes.Use2;
                shopItem.PriceType = ShopPriceType.Parse(itemRes.PriceType);
                shopItem.Price0 = itemRes.Price0;
                shopItem.Price1 = itemRes.Price1;
                shopItem.Price2 = itemRes.Price2;
                shopItem.OldPrice0 = itemRes.OldPrice0;
                shopItem.OldPrice1 = itemRes.OldPrice1;
                shopItem.OldPrice2 = itemRes.OldPrice2;
                shopItem.CouplePrice = itemRes.CouplePrice;
                shopItem.CategoryType = ShopCategoryType.Parse(itemRes.Category);
                shopItem.Name = itemRes.Name;
                shopItem.GoldBack = itemRes.GoldBack;
                shopItem.EnableParcel = itemRes.EnableParcel == 1;
                shopItem.Hero = (int)itemRes.Char;
                shopItem.Item0 = itemRes.Item0;
                shopItem.Item1 = itemRes.Item1;
                shopItem.Item2 = itemRes.Item2;
                shopItem.Item3 = itemRes.Item3;
                shopItem.Item4 = itemRes.Item4;
                shopItem.Item5 = itemRes.Item5;
                shopItem.Item6 = itemRes.Item6;
                shopItem.Item7 = itemRes.Item7;
                shopItem.Item8 = itemRes.Item8;
                shopItem.Item9 = itemRes.Item9;
                tmp.Add(shopItem.Index, shopItem);
            }

            foreach (var kvp in tmp.OrderBy(p => p.Key))
                Add(kvp.Key, kvp.Value);

            _itemPartDataStore = itemPartDataStore;
            _houseDecorationDataStore = houseDecorationDataStore;
            _enchantDataStore = enchantDataStore;
            _recipeDataStore = recipeDataStore;
            logger.LogInformation("loaded.");
        }

        public IEnumerable<ShopItem> Search(int categoryType, int partType, int heroType)
        {
            var list = Values.AsEnumerable();
            list = list.Where(p => p.Enable && p.CategoryType == categoryType);
            if (categoryType == ShopCategoryType.Parts)
            {
                if (partType == ItemPartType.Set)
                {
                    var set = _itemPartDataStore.ByHero(heroType);
                    list = list.Where(p => p.Item0 > 0 && p.Item1 > 0 && set.Contains(p.Item1));
                }
                else
                {
                    var set = _itemPartDataStore.ByTypeAndHero(partType, heroType);
                    list = list.Where(p => p.Item1 == 0 && set.Contains(p.Item0));
                }
            }
            else if (categoryType == ShopCategoryType.HouseDecoration)
            {
                var set = _houseDecorationDataStore.ByKind(partType);
                list = list.Where(p => set.Contains(p.Item0));
            }
            else if (categoryType == ShopCategoryType.Recipe)
            {
                var set = _recipeDataStore.ByKindAndHero(partType, heroType);
                list = list.Where(p => set.Contains(p.Item0));
            }
            else if (categoryType == ShopCategoryType.Enchant)
            {
                var set = _enchantDataStore.ByKind(partType);
                list = list.Where(p => set.Contains(p.Item0));
            }
            else if (categoryType == ShopCategoryType.Lottery)
            {
                list = list.Where(p => p.PriceType == partType);
            }
            return list;
        }
    }
}
