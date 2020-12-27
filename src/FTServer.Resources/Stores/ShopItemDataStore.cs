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
                shopItem.CategoryType = ItemCategoryType.Parse(itemRes.Category);
                shopItem.Name = itemRes.Name;
                shopItem.GoldBack = itemRes.GoldBack == 1;
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
                Add(shopItem.Index, shopItem);
            }
        }
    }
}
