using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using System.Collections.Generic;

namespace FTServer.Resources.Stores.Pet
{
    public class ItemPartDataStore : Dictionary<int, ItemPart>, IItemPartDataStore
    {
        private const string Resource = "Res/Script/Item/Item_Parts.xml";

        public ItemPartDataStore(IResourceManager resourceManager)
        {
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            foreach (dynamic itemRes in resource.ItemList)
            {
                var itemPart = new ItemPart();
                itemPart.Index = itemRes.Index;
                itemPart.Name = itemRes.Nameen;
                itemPart.Name = itemPart.Name.Trim();
                itemPart.Hero = HeroType.Parse(itemRes.Char);
                itemPart.Type = ItemPartType.Parse(itemRes.Part);
                itemPart.Mesh = itemRes.Mesh;
                itemPart.Hair = itemRes.Hair;
                itemPart.Leg = itemRes.Leg;
                itemPart.Foot = itemRes.Foot;
                itemPart.EnchantElement = itemRes.EnchantElement;
                Add(itemPart.Index, itemPart);
            }
        }
    }
}
