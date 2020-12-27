namespace FTServer.Resources
{
    public class ItemHouseDecoration
    {
        public int Index { get; set; }
        public HouseDecorationKind Kind { get; set; }
        public ItemUseType UseType { get; set; }
        public int MaxUse { get; set; }
        public int HousingPoint { get; set; }
        public bool InHouse { get; set; }
        public int AddGold { get; set; }
        public int AddExp { get; set; }
        public int AddBattleGold { get; set; }
        public int AddBattleExp { get; set; }
        public bool EnableParcel { get; set; }
        public string Name { get; set; }
    }
}
