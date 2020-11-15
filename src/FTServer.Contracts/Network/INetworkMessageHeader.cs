using FTServer.Contracts.MemoryManagement;

namespace FTServer.Contracts.Network
{
    public interface INetworkMessageHeader : ISerializableMemory, ISerializableRawMemory
    {
        ushort Serial { get; set; }
        ushort Checksum { get; set; }
        ushort MessageId { get; set; }
        ushort BodyLength { get; set; }
    }
}
