namespace FTServer.Contracts.Network
{
    public interface ISerializableRawMemory
    {
        int Serialize(byte[] buffer, int offset);
        void Deserialize(byte[] buffer, int offset);
    }
}
