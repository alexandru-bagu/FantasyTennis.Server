using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class LoginAttempt : DbEntity<int>
    {
        public string Ip { get; set; }
        public int LoginId { get; set; }
        public bool Successful { get; set; }
        public DateTime Timestamp { get; set; }
        [ForeignKey(nameof(LoginId))] public Login Login { get; set; }

        public LoginAttempt() { }
        public LoginAttempt(string ip, int loginId, bool successful)
        {
            Ip = ip;
            LoginId = loginId;
            Successful = successful;
            Timestamp = DateTime.Now;
        }
    }
}
