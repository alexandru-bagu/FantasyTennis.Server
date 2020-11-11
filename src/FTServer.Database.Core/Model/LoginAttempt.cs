using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Core.Model
{
    public class LoginAttempt : DbEntity<int>
    {
        public string Ip { get; set; }
        public int LoginId { get; set; }
        public bool Successful { get; set; }
        [ForeignKey(nameof(LoginId))] public Login Login { get; set; }
    }
}
