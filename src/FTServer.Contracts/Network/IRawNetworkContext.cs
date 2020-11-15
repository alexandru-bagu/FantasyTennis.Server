using System.ComponentModel;
using System.Threading.Tasks;

namespace FTServer.Contracts.Network
{
    public interface IRawNetworkContext
    {
        INetworkContextOptions Options { get; }
        Task SendRawAsync(byte[] buffer, int offset, int size);
    }
}
