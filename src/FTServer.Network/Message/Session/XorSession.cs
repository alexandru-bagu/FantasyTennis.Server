using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message.Login
{
    [NetworkMessage(MessageId)]
    public class XorSession : NetworkMessage
    {
        public const ushort MessageId = 0xFF9A;
        public override int MaximumSize => 24;

        public int DecryptionKey { get; set; }
        public int EncryptionKey { get; set; }
        public int DecryptionSerialKey { get; set; }
        public int EncryptionSerialKey { get; set; }

        public XorSession() : base(MessageId)
        {
        }

        public XorSession(int decryptionKey, int encryptionKey, int decryptionSerialKey, int encryptionSerialKey) : this()
        {
            DecryptionKey = decryptionKey;
            EncryptionKey = encryptionKey;
            DecryptionSerialKey = decryptionSerialKey;
            EncryptionSerialKey = encryptionSerialKey;
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            DecryptionKey = reader.ReadInt32();
            EncryptionKey = reader.ReadInt32();
            DecryptionSerialKey = reader.ReadInt32();
            EncryptionSerialKey = reader.ReadInt32();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(DecryptionKey);
            writer.Write(EncryptionKey);
            writer.Write(DecryptionSerialKey);
            writer.Write(EncryptionSerialKey);
        }
    }
}
