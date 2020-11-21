using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message.System.Session
{
    /// <summary>
    /// Message to initialize xor crypto client-side. This message will include both encryption and decryption serial keys and so serial numbers will matter.
    /// </summary>
    [NetworkMessage(MessageId)]
    public class XorSessionHello : NetworkMessage
    {
        public const ushort MessageId = 0xFF9A;
        public override int MaximumSize => 24;

        public int EncryptionKey { get; set; }
        public int DecryptionKey { get; set; }
        public int EncryptionSerialKey { get; set; }
        public int DecryptionSerialKey { get; set; }

        public XorSessionHello() : base(MessageId)
        {
        }

        public XorSessionHello(int encryptionKey, int decryptionKey, int encryptionSerialKey, int decryptionSerialKey) : this()
        {
            EncryptionKey = encryptionKey;
            DecryptionKey = decryptionKey;
            EncryptionSerialKey = encryptionSerialKey;
            DecryptionSerialKey = decryptionSerialKey;
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            EncryptionKey = reader.ReadInt32();
            DecryptionKey = reader.ReadInt32();
            EncryptionSerialKey = reader.ReadInt32();
            DecryptionSerialKey = reader.ReadInt32();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(EncryptionKey);
            writer.Write(DecryptionKey);
            writer.Write(EncryptionSerialKey);
            writer.Write(DecryptionSerialKey);
        }
    }
}
