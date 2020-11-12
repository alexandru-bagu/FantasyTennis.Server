using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class Item : DbEntity<int>
    {
        public ItemCategory Category { get; set; }
        public int ItemTypeId { get; set; }
        public ItemUseType UseType { get; set; }
        public int Quantity { get; set; }
        public int CharacterId { get; set; }
        [ForeignKey(nameof(CharacterId))] public Character Character { get; set; }
    }
}
