using System.Collections.Generic;

namespace FTServer.Resources.EmblemQuest
{
    public class Quest
    {
        public int Index { get; set; }
        public bool Enable { get; set; }
        public bool Event { get; set; }
        public int EmblemGrade { get; set; }
        public string NameLabel { get; set; }
        public string SuccessCondition { get; set; }
        public int QuestDetail { get; set; }
        public List<int> QuestPreRequisite { get; set; }
        public int RewardExperience { get; set; }
        public int RewardGold { get; set; }
        public bool QuestRepeat { get; set; }
        public bool ItemRewardRepeat { get; set; }
        public List<int> RewardItems { get; set; }
        public Quest()
        {
            QuestPreRequisite = new List<int>();
            RewardItems = new List<int>();
        }
    }
}
