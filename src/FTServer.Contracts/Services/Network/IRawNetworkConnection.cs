using System.ComponentModel;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Network
{
    public interface IRawNetworkConnection
    {
        [EditorBrowsable(EditorBrowsableState.Never)] Task SendRawAsync(byte[] buffer, int offset, int size);
    }
}
