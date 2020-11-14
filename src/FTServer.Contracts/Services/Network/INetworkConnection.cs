using FTServer.Contracts.Network;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Network
{
    public interface INetworkConnection : IRawNetworkConnection
    {
        Task SendAsync(INetworkMessage message);
        Task DisconnectAsync();
    }
}
