using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class Home : DbEntity<int>
    {
        public Home()
        {
            Furniture = new HashSet<Furniture>();
        }
        public int CharacterId { get; set; }
        public byte Level { get; set; }
        public int HousingPoints { get; set; }
        public int FamousPoints { get; set; }
        public int BasicBonusExp() => 0;
        public int BasicBonusGold() => 0;
        public int BattleBonusExp() => 0;
        public int BattleBonusGold() => 0;
        [ForeignKey(nameof(CharacterId))] public Character Character { get; set; }
        public HashSet<Furniture> Furniture { get; set; }
    }
}
