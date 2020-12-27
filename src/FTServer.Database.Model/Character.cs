using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class Character : DbEntity<int>
    {
        public int MaximumInventoryCount { get; set; }
        public byte Level { get; set; }
        public int Experience { get; set; }
        public bool Enabled { get; set; }
        public int Gold { get; set; }
        public int Type { get; set; }

        public byte Strength { get; set; }
        public byte Stamina { get; set; }
        public byte Dexterity { get; set; }
        public byte Willpower { get; set; }
        public byte StatusPoints { get; set; }

        public string Name { get; set; }
        public bool IsCreated { get; set; }
        public bool NameChangeAllowed { get; set; }
        public bool NameChangeByIcon { get; set; }

        /// <summary>
        /// CCharacter::UnknownByte1
        /// </summary>
        [NotMapped] public byte UnknownByte1 { get; set; }
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    }
}
