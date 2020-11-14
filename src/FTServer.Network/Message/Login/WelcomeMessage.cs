using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message.Login
{
    public class WelcomeMessage : NetworkMessage
    {
        public override int MaximumSize => 24;
        public override int MessageId { get => 1; }

        public int DecryptionKey { get; set; }
        public int EncryptionKey { get; set; }
        public int DecryptionTableIndex { get; set; }
        public int EncryptionTableIndex { get; set; }

        public WelcomeMessage(int decryptionKey, int encryptionKey, int decryptionTableIndex, int encryptionTableIndex)
        {
            DecryptionKey = decryptionKey;
            EncryptionKey = encryptionKey;
            DecryptionTableIndex = decryptionTableIndex;
            EncryptionTableIndex = encryptionTableIndex;
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            DecryptionKey = reader.ReadInt32();
            EncryptionKey = reader.ReadInt32();
            DecryptionTableIndex = reader.ReadInt32();
            EncryptionTableIndex = reader.ReadInt32();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            writer.Write(DecryptionKey);
            writer.Write(EncryptionKey);
            writer.Write(DecryptionTableIndex);
            writer.Write(EncryptionTableIndex);
        }
    }
}
