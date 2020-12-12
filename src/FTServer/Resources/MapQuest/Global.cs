using System.Collections.Generic;

namespace FTServer.Resources.MapQuest
{
    public class Global
    {
        public int MapLocation { get; set; }
        public QuestGameType GameType { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public List<int> IndexCondition { get; set; }
    }
}
