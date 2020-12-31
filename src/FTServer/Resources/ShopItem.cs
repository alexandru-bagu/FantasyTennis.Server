namespace FTServer.Resources
{
    public class ShopItem
    {
        public int Display { get; set; }
        public int Index { get; set; }
        public bool Enable { get; set; }
        public bool Free { get; set; }
        public bool Sale { get; set; }
        public bool Event { get; set; }
        public bool Couple { get; set; }
        public bool Nobuy { get; set; }
        public bool Rand { get; set; }
        public ItemUseType UseType { get; set; }
        public int Use0 { get; set; }
        public int Use1 { get; set; }
        public int Use2 { get; set; }
        public ShopPriceType PriceType { get; set; }
        public int Price0 { get; set; }
        public int Price1 { get; set; }
        public int Price2 { get; set; }
        public int OldPrice0 { get; set; }
        public int OldPrice1 { get; set; }
        public int OldPrice2 { get; set; }
        public int CouplePrice { get; set; }
        public ShopCategoryType CategoryType { get; set; }
        public string Name { get; set; }
        public int GoldBack { get; set; }
        public bool EnableParcel { get; set; }
        public HeroType Hero { get; set; }
        public int[] Items { get; set; }
        public int Item0 { get { if (Items.Length > 0) return Items[0]; return 0; } }
        public int Item1 { get { if (Items.Length > 1) return Items[1]; return 0; } }
    }
}
