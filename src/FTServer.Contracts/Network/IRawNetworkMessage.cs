
namespace FTServer.Contracts.Network
{
    public interface IRawNetworkMessage : ISerializableRawMemory
    {
        int MaximumSize { get; }
    }
}
