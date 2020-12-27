namespace FTServer.Resources.MapQuest
{
    public class Tutorial
    {
        public ushort Index { get; set; }
        public string Name { get; set; }
        public int RewardExperience { get; set; }
        public int RewardGold { get; set; }
        public bool ItemRewardRepeat { get; set; }
        public int RewardItem1 { get; set; }
        public int QuantityMin1 { get; set; }
        public int QuantityMax1 { get; set; }
        public int RewardItem2 { get; set; }
        public int QuantityMin2 { get; set; }
        public int QuantityMax2 { get; set; }
        public int RewardItem3 { get; set; }
        public int QuantityMin3 { get; set; }
        public int QuantityMax3 { get; set; }
        public string SuccessCondition { get; set; }
    }
}
