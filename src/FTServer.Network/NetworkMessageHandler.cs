using FTServer.Contracts.Network;
using System.Threading.Tasks;

namespace FTServer.Network
{
    public abstract class NetworkMessageHandler<TNetworkContext, TNetworkMessage> : INetworkMessageHandler<TNetworkContext>
        where TNetworkContext : INetworkContext
    {
        public abstract Task Process(TNetworkMessage message, TNetworkContext context);

        public Task Process(INetworkMessage message, TNetworkContext context)
        {
            return Process((TNetworkMessage)message, context);
        }
    }
}
