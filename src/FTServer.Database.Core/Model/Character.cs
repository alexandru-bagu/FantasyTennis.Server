using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Core.Model
{
    public class Character : DbEntity<int>
    {
        public Character()
        {
            Items = new HashSet<Item>();
        }

        public int MaximumInventoryCount { get; set; }
        public int Experience { get; set; }
        public bool Enabled { get; set; }
        public int Gold { get; set; }
        public CharacterType Type { get; set; }

        public byte Strength { get; set; }
        public byte Stamina { get; set; }
        public byte Dexterity { get; set; }
        public byte Willpower { get; set; }
        public byte StatusPoints { get; set; }

        public bool NameChangeAllowed { get; set; }

        public int HairId { get; set; }
        public int FaceId { get; set; }
        public int DressId { get; set; }
        public int PantsId { get; set; }
        public int SocksId { get; set; }
        public int ShoesId { get; set; }
        public int GlovesId { get; set; }
        public int RacketId { get; set; }
        public int GlassesId { get; set; }
        public int BagId { get; set; }
        public int HatId { get; set; }
        public int DyeId { get; set; }
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
        public HashSet<Item> Items { get; set; }
        public HashSet<Home> Homes { get; set; }
    }
}
