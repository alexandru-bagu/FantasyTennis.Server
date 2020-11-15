using System.Threading.Tasks;

namespace FTServer.Contracts.Network
{
    public interface INetworkMessageHandler<TNetworkContext>
        where TNetworkContext : INetworkContext
    {
        Task Process(INetworkMessage message, TNetworkContext context);
    }
}
