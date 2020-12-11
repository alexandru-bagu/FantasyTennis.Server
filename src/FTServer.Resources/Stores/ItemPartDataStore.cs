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

            foreach (dynamic item in resource.ItemList)
            {
                var itemPart = new ItemPart();
                itemPart.Index = item.Index;
                itemPart.Name = item.Nameen;
                itemPart.Name = itemPart.Name.Trim();
                itemPart.Hero = HeroType.Parse(item.Char);
                itemPart.Type = ItemPartType.Parse(item.Part);
                itemPart.Mesh = item.Mesh;
                itemPart.Hair = item.Hair;
                itemPart.Leg = item.Leg;
                itemPart.Foot = item.Foot;
                itemPart.EnchantElement = item.EnchantElement;
                Add(itemPart.Index, itemPart);
            }
        }
    }
}
