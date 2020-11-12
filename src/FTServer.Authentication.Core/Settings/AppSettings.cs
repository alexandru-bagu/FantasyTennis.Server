using FTServer.Core.Settings;

namespace FTServer.Authentication.Core.Settings
{
    public class AppSettings : CoreSettings
    {
        public AuthServerSettings AuthServer { get; set; }
    }
}
