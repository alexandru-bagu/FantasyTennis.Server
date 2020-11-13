using System.ComponentModel;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Network
{
    public interface INetworkConnectionNotification
    {
        [EditorBrowsable(EditorBrowsableState.Never)] Task NotifyConnected();
        [EditorBrowsable(EditorBrowsableState.Never)] Task NotifyDisconnected();
    }
}
