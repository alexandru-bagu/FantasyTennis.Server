namespace FTServer.Contracts.Network
{
    public interface INetworkMessage : IRawNetworkMessage, ISerializableMemory
    {
        INetworkMessageHeader Header { get; set; }
    }
}
