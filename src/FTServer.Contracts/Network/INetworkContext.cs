using FTServer.Contracts.Network;
using System.Threading.Tasks;

namespace FTServer.Contracts.Network
{
    public interface INetworkContext : IRawNetworkContext, INetworkContextNotification
    {
        Task SendAsync(INetworkMessage message);
        Task DisconnectAsync();
    }
}
