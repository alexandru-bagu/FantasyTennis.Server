using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class Login : DbEntity<int>
    {
        public int AccountId { get; set; }

        [Required] public string Username { get; set; }
        [Required] public string Hash { get; set; }
        [Required] public string Salt { get; set; }
        [Required] [EmailAddress] public string Email { get; set; }
        public DateTime? DisabledUntil { get; set; }

        [ForeignKey(nameof(AccountId))] public Account Account { get; set; }
    }
}
