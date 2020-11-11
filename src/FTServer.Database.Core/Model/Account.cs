using System.Collections.Generic;

namespace FTServer.Database.Core.Model
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

        public HashSet<Login> Logins { get; set; }
        public HashSet<Character> Characters { get; set; }
    }
}
