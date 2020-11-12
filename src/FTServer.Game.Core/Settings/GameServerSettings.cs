using FTServer.Core.Settings;

namespace FTServer.Game.Core.Settings
{
    public class GameServerSettings
    {
        public string Name { get; set; }
        public NetworkSettings Network { get; set; }
    }
}
