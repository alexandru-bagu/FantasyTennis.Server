using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message.Login
{
    [NetworkMessage(MessageId)]
    public class WelcomeMessage : NetworkMessage
    {
        public const ushort MessageId = 0xFF9A;
        public override int MaximumSize => 24;

        public int DecryptionKey { get; set; }
        public int EncryptionKey { get; set; }
        public int DecryptionTableIndex { get; set; }
        public int EncryptionTableIndex { get; set; }

        public WelcomeMessage() : base(MessageId)
        {
        }

        public WelcomeMessage(int decryptionKey, int encryptionKey, int decryptionTableIndex, int encryptionTableIndex) : this()
        {
            DecryptionKey = decryptionKey;
            EncryptionKey = encryptionKey;
            DecryptionTableIndex = decryptionTableIndex;
            EncryptionTableIndex = encryptionTableIndex;
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            DecryptionKey = reader.ReadInt32();
            EncryptionKey = reader.ReadInt32();
            DecryptionTableIndex = reader.ReadInt32();
            EncryptionTableIndex = reader.ReadInt32();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(DecryptionKey);
            writer.Write(EncryptionKey);
            writer.Write(DecryptionTableIndex);
            writer.Write(EncryptionTableIndex);
        }
    }
}
