using FTServer.Core.Settings;

namespace FTServer.Relay.Core.Settings
{
    public class AppSettings : CoreSettings
    {
        public RelayServerSettings RelayServer { get; set; }
    }
}
