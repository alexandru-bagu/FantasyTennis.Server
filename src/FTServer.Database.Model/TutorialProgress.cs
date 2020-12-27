using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class TutorialProgress : DbEntity<int>
    {
        public ushort TutorialId { get; set; }
        public int CharacterId { get; set; }
        public int Completed { get; set; }
        public int Attempts { get; set; }
        [ForeignKey(nameof(CharacterId))] public Character Character { get; set; }
    }
}
