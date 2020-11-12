using FTServer.Core.Settings;

namespace FTServer.Game.Core.Settings
{
    public class AppSettings : CoreSettings
    {
        public GameServerSettings GameServer { get; set; }
    }
}
