namespace FTServer.Resources.Quest
{
    public class ChallengeBase : Tutorial
    {
        public int Level { get; set; }
        public int LevelRestriction { get; set; }
        public GameMode GameMode { get; set; }
    }
}
