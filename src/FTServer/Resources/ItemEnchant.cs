namespace FTServer.Resources
{
    public class ItemEnchant
    {
        public int Index { get; set; }
        public ItemUseType UseType { get; set; }
        public int MaxUse { get; set; }
        public EnchantKind Kind { get; set; }
        public ElementalKind ElementalKind { get; set; }
        public int AddPer { get; set; }
        public int SellPrice { get; set; }
        public bool Hair { get; set; }
        public bool Body { get; set; }
        public bool Pants { get; set; }
        public bool Foot { get; set; }
        public bool Cap { get; set; }
        public bool Hand { get; set; }
        public bool Glasses { get; set; }
        public bool Bag { get; set; }
        public bool Socks { get; set; }
        public bool Racket { get; set; }
        public string Name { get; set; }
    }
}
