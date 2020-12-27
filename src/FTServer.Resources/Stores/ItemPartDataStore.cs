using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Resources.Stores
{
    public class ItemPartDataStore : Dictionary<int, ItemPart>, IItemPartDataStore
    {
        private const string Resource = "Res/Script/Item/Item_Parts.xml";
        private Dictionary<int, HashSet<int>> _byType;
        private Dictionary<HeroType, HashSet<int>> _byHero;

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
            _byType = new Dictionary<int, HashSet<int>>();
            _byHero = new Dictionary<HeroType, HashSet<int>>();
        }

        public HashSet<int> ByHero(HeroType hero)
        {
            if (!_byHero.TryGetValue(hero, out HashSet<int> set))
                _byHero.Add(hero, set = Values.Where(p => p.Hero == hero).Select(p => p.Index).ToHashSet());
            return set;
        }

        public HashSet<int> ByTypeAndHero(ShopItemPartType type, HeroType hero)
        {
            var key = 1000 + type * 1000 + hero;
            if (!_byType.TryGetValue(key, out HashSet<int> set))
                _byType.Add(key, set = Values.Where(p => p.Type == type && p.Hero == hero).Select(p => p.Index).ToHashSet());
            return set;
        }
    }
}
