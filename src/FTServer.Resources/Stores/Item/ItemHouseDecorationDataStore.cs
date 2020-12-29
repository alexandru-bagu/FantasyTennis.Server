using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Item;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Resources.Stores.Item
{
    public class ItemHouseDecorationDataStore : Dictionary<int, ItemHouseDecoration>, IItemHouseDecorationDataStore
    {
        private const string Resource = "Res/Script/Item/Item_HouseDeco.xml";
        private Dictionary<HouseDecorationKind, HashSet<int>> _byKind;

        public ItemHouseDecorationDataStore(IResourceManager resourceManager)
        {
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            foreach (dynamic houseDecoRes in resource.ItemList)
            {
                var houseDeco = new ItemHouseDecoration();
                houseDeco.Index = houseDecoRes.Index;
                houseDeco.Name = houseDecoRes.Nameen;
                houseDeco.Name = houseDeco.Name.Trim();
                houseDeco.Kind = HouseDecorationKind.Parse(houseDecoRes.Kind);
                houseDeco.UseType = ItemUseType.Parse(houseDecoRes.UseType);
                houseDeco.MaxUse = houseDecoRes.MaxUse;
                houseDeco.HousingPoint = houseDecoRes.Housingpoint;
                houseDeco.InHouse = houseDecoRes.InHouse == 1;
                houseDeco.AddGold = houseDecoRes.AddGold;
                houseDeco.AddExp = houseDecoRes.AddExp;
                houseDeco.AddBattleGold = houseDecoRes.AddBattleGold;
                houseDeco.AddBattleExp = houseDecoRes.AddBattleExp;
                houseDeco.EnableParcel = houseDecoRes.EnableParcel == 1;
                Add(houseDeco.Index, houseDeco);
            }
            _byKind = new Dictionary<HouseDecorationKind, HashSet<int>>();
        }

        public HashSet<int> ByKind(HouseDecorationKind kind)
        {
            if (!_byKind.TryGetValue(kind, out HashSet<int> set))
                _byKind.Add(kind, set = Values.Where(p => p.Kind == kind).Select(p => p.Index).ToHashSet());
            return set;
        }
    }
}
