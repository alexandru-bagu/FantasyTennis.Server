using FTServer.Contracts.MemoryManagement;

namespace FTServer.Contracts.Network
{
    public interface ISerializableMemory
    {
        void Serialize(IUnmanagedMemoryWriter writer);
        void Deserialize(IUnmanagedMemoryReader reader);
    }
}
