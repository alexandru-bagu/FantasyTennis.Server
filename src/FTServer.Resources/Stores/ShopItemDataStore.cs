using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using System.Collections.Generic;

namespace FTServer.Resources.Stores.Pet
{
    public class ShopItemDataStore : Dictionary<int, ShopItem>, IShopItemDataStore
    {
        private const string Resource = "Res/Script/Shop/Ini3/Shop_Ini3.xml";

        public ShopItemDataStore(IResourceManager resourceManager)
        {
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            foreach (dynamic item in resource.ProductList)
            {
                var shopItem = new ShopItem();
                shopItem.Display = item.DISPLAY;
                shopItem.Index = item.Index;
                shopItem.Enable = item.Enable == 1;
                shopItem.Free = item.Free == 1;
                shopItem.Sale = item.Sale == 1;
                shopItem.Event = item.Event == 1;
                shopItem.Couple = item.Couple == 1;
                shopItem.Nobuy = item.Nobuy == 1;
                shopItem.Rand = item.Rand != "No";
                shopItem.UseType = ShopItemUseType.Parse(item.UseType);
                shopItem.Use0 = item.Use0;
                shopItem.Use1 = item.Use1;
                shopItem.Use2 = item.Use2;
                shopItem.PriceType = ShopPriceType.Parse(item.PriceType);
                shopItem.Price0 = item.Price0;
                shopItem.Price1 = item.Price1;
                shopItem.Price2 = item.Price2;
                shopItem.OldPrice0 = item.OldPrice0;
                shopItem.OldPrice1 = item.OldPrice1;
                shopItem.OldPrice2 = item.OldPrice2;
                shopItem.CouplePrice = item.CouplePrice;
                shopItem.CategoryType = ShopCategoryType.Parse(item.Category);
                shopItem.Name = item.Name;
                shopItem.GoldBack = item.GoldBack == 1;
                shopItem.EnableParcel = item.EnableParcel == 1;
                shopItem.HeroType = (int)item.Char;
                shopItem.Item0 = item.Item0;
                shopItem.Item1 = item.Item1;
                shopItem.Item2 = item.Item2;
                shopItem.Item3 = item.Item3;
                shopItem.Item4 = item.Item4;
                shopItem.Item5 = item.Item5;
                shopItem.Item6 = item.Item6;
                shopItem.Item7 = item.Item7;
                shopItem.Item8 = item.Item8;
                shopItem.Item9 = item.Item9;
                Add(shopItem.Index, shopItem);
            }
        }
    }
}
