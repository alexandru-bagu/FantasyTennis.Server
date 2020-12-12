namespace FTServer.Resources.EmblemQuest
{
    public class RewardItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public HeroType Hero { get; set; }
        public int ShopIndex { get; set; }
        public int QuantityMin { get; set; }
        public int QuantityMax { get; set; }
    }
}
