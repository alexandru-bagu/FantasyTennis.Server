using System;

namespace FTServer.Database.Model
{
    public class RelayServer : DbEntity<ushort>
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public DateTime Heartbeat { get; set; }
        public string Host { get; set; }
        public ushort Port { get; set; }
    }
}
