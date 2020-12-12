namespace FTServer.Dto
{
    public class FriendDto
    {
        public int FriendshipId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public HeroType Type { get; set; }
        public short ActiveServer { get; set; }
    }
}
