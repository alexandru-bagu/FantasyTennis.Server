using FTServer.Contracts.Network;

namespace FTServer.Contracts.Services.Network
{
    public interface INetworkMessageFactory
    {
        INetworkMessage Create(byte[] buffer, int offset, int size);
    }
}
