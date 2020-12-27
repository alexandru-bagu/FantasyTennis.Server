using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class Item : DbEntity<int>
    {
        public int Index { get; set; }
        public byte CategoryType { get; set; }
        [NotMapped] public ShopCategoryType ItemCategoryType { get { return CategoryType; } set { CategoryType = value; } }
        public byte UseType { get; set; }
        [NotMapped] public ItemUseType ItemUseType { get { return UseType; } set { UseType = value; } }
        public bool Equipped { get; set; }
        public int Quantity { get; set; }
        public int CharacterId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public byte EnchantStrength { get; set; }
        public byte EnchantStamina { get; set; }
        public byte EnchantDexterity { get; set; }
        public byte EnchantWillpower { get; set; }
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        [ForeignKey(nameof(CharacterId))] public Character Character { get; set; }
    }
}
