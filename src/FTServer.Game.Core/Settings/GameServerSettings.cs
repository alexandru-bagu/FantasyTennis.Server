using FTServer.Core.Settings;

namespace FTServer.Game.Core.Settings
{
    public class GameServerSettings
    {
        public string Name { get; set; }
        public bool ShowName { get; set; }
        public GameServerType Type { get; set; }
        public NetworkSettings Network { get; set; }
    }
}
