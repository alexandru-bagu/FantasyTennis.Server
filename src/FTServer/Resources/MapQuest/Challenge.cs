namespace FTServer.Resources.MapQuest
{
    public class Challenge : TennisChallenge
    {
        public int NpcIndex { get; set; }
        public int AILevel { get; set; }
        public int Hitpoints { get; set; }
        public byte Strength { get; set; }
        public byte Stamina { get; set; }
        public byte Dexterity { get; set; }
        public byte Willpower { get; set; }
    }
}
