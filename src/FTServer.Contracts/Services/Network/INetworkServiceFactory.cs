using FTServer.Contracts.Network;

namespace FTServer.Contracts.Services.Network
{
    public interface INetworkServiceFactory
    {
        INetworkService<T> Create<T>(string host, int port) 
            where T : INetworkContext;
    }
}
