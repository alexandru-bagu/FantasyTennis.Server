using System.Collections.Generic;

namespace FTServer.Resources.EmblemQuest
{
    public class QuestCondition
    {
        public QuestConditionType Type { get; private set; }
        public List<int> Count { get; private set; }

        public QuestCondition(QuestConditionType type, List<int> count)
        {
            Type = type;
            Count = count;
        }
    }
}
