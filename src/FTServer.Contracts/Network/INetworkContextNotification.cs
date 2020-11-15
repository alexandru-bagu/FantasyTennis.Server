using System.ComponentModel;
using System.Threading.Tasks;

namespace FTServer.Contracts.Network
{
    public interface INetworkContextNotification
    {
        [EditorBrowsable(EditorBrowsableState.Never)] Task NotifyConnected();
        [EditorBrowsable(EditorBrowsableState.Never)] Task NotifyDisconnected();
    }
}
