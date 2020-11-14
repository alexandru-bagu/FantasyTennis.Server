using FTServer.Contracts.MemoryManagement;

namespace FTServer.Contracts.Network
{
    public interface IRawNetworkMessage
    {
        void Serialize(byte[] buffer, int offset);
        void Deserialize(byte[] buffer, int offset);
    }
}
