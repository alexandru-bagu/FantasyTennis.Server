using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FTServer.Database.Model
{
    public class GameServer : DbEntity<short>
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public DateTime Heartbeat { get; set; }
        public string Host { get; set; }
        public ushort Port { get; set; }
        public bool ShowName { get; set; }
        public GameServerType Type { get; set; }
        public ushort OnlineCount { get; set; }
        [NotMapped] public byte UnknownByte { get; set; }
    }
}
