using System.Collections.Generic;

namespace FTServer.Resources.EmblemQuest
{
    public class QuestDetail
    {
        public int Index { get; set; }
        public GameMode GameMode { get; set; }
        public int LevelRestriction { get; set; }
        public List<QuestCondition> Conditions { get; set; }
        public List<QuestItemRequirement> ItemRequirement { get; set; }

        public QuestDetail()
        {
            Conditions = new List<QuestCondition>();
            ItemRequirement = new List<QuestItemRequirement>();
        }
    }
}
