using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class Friendship : DbEntity<int>
    {
        public int HeroOneId { get; set; }
        public int HeroTwoId { get; set; }
        [ForeignKey(nameof(HeroOneId))] public Character HeroOne { get; set; }
        [ForeignKey(nameof(HeroTwoId))] public Character HeroTwo { get; set; }
    }
}
