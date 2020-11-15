using FTServer.Contracts.Network;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Network
{
    public interface INetworkMessageHandlerService<TNetworkContext> 
        where TNetworkContext : INetworkContext
    {
        void RegisterDefaultHandler(INetworkMessageHandler<TNetworkContext> defaultHandler);
        void UnregisterDefaultHandler(INetworkMessageHandler<TNetworkContext> defaultHandler);
        Task Process(INetworkMessage message, TNetworkContext context);
    }
}
