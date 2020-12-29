using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Item;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Resources.Stores.Item
{
    public class ItemEnchantDataStore : Dictionary<int, ItemEnchant>, IItemEnchantDataStore
    {
        private const string Resource = "Res/Script/Item/Item_Enchant.xml";
        private Dictionary<EnchantKind, HashSet<int>> _byKind;

        public ItemEnchantDataStore(IResourceManager resourceManager)
        {
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            foreach (dynamic enchantRes in resource.ItemList)
            {
                var enchant = new ItemEnchant();
                enchant.Index = enchantRes.Index;
                enchant.Name = enchantRes.Nameen;
                enchant.Name = enchant.Name.Trim();
                enchant.Kind = EnchantKind.Parse(enchantRes.Kind);
                enchant.UseType = ItemUseType.Parse(enchantRes.UseType);
                enchant.MaxUse = enchantRes.MaxUse;
                enchant.ElementalKind = ElementalKind.Parse(enchantRes.ElementalKind);
                enchant.AddPer = enchantRes.AddPer;
                enchant.SellPrice = enchantRes.SellPrice;
                enchant.Hair = enchantRes.HAIR == 1;
                enchant.Body = enchantRes.BODY == 1;
                enchant.Pants = enchantRes.PANTS == 1;
                enchant.Foot = enchantRes.FOOT == 1;
                enchant.Cap = enchantRes.CAP == 1;
                enchant.Hand = enchantRes.HAND == 1;
                enchant.Glasses = enchantRes.GLASSES == 1;
                enchant.Bag = enchantRes.BAG == 1;
                enchant.Socks = enchantRes.SOCKS == 1;
                enchant.Racket = enchantRes.RACKET == 1;
                Add(enchant.Index, enchant);
            }
            _byKind = new Dictionary<EnchantKind, HashSet<int>>();
        }

        public HashSet<int> ByKind(EnchantKind kind)
        {
            if (!_byKind.TryGetValue(kind, out HashSet<int> set))
                _byKind.Add(kind, set = Values.Where(p => p.Kind == kind).Select(p => p.Index).ToHashSet());
            return set;
        }
    }
}
