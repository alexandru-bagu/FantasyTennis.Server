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
        public ShopItemUseType UseType { get; set; }
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
        public bool GoldBack { get; set; }
        public bool EnableParcel { get; set; }
        public HeroType HeroType { get; set; }
        public int Item0 { get; set; }
        public int Item1 { get; set; }
        public int Item2 { get; set; }
        public int Item3 { get; set; }
        public int Item4 { get; set; }
        public int Item5 { get; set; }
        public int Item6 { get; set; }
        public int Item7 { get; set; }
        public int Item8 { get; set; }
        public int Item9 { get; set; }
    }
}
