using FTServer.Contracts.MemoryManagement;

namespace FTServer.Contracts.Network
{
    public interface INetworkMessage : IRawNetworkMessage
    {
        int MaximumSize { get; }
        void Serialize(IUnmanagedMemoryWriter writer);
        void Deserialize(IUnmanagedMemoryReader reader);
    }
}
