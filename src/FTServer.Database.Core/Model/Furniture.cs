using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Core.Model
{
    public class Furniture : DbEntity<int>
    {
        public int HomeId { get; set; }
        public int ItemTypeId { get; set; }
        public byte Unknown0 { get; set; }
        public byte Unknown1 { get; set; }
        public byte X { get; set; }
        public byte Y { get; set; }
        [ForeignKey(nameof(HomeId))] public Home Home { get; set; }
    }
}
