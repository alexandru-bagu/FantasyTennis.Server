using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class Account : DbEntity<int>
    {
        public Account()
        {
            Logins = new HashSet<Login>();
            Characters = new HashSet<Character>();
        }

        public int Ap { get; set; }
        public bool Enabled { get; set; }
        public bool Online { get; set; }
        public short? ActiveServerId { get; set; }
        public SecurityLevel SecurityLevel { get; set; }
        public int LastCharacterId { get; set; }

        public int Key1 { get; set; }
        public int Key2 { get; set; }

        public HashSet<Login> Logins { get; set; }
        public HashSet<Character> Characters { get; set; }
        [ForeignKey(nameof(ActiveServerId))] public GameServer ActiveServer { get; set; }
    }
}
