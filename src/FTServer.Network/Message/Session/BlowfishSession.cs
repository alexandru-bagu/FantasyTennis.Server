using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message.Login
{
    [NetworkMessage(MessageId)]
    public class BlowfishSession : NetworkMessage
    {
        public const ushort MessageId = 0xFF99;
        public override int MaximumSize => 40;

        public byte[] BlowfishSendKey { get; set; }
        public byte[] BlowfishReceiveKey { get; set; }

        public BlowfishSession() : base(MessageId)
        {
        }

        public BlowfishSession(byte[] blowfishSendKey, byte[] blowishReceiveKey) : this()
        {
            BlowfishSendKey = blowfishSendKey;
            BlowfishReceiveKey = blowishReceiveKey;
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            BlowfishSendKey = reader.ReadBytes(16);
            BlowfishReceiveKey = reader.ReadBytes(16);
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(BlowfishSendKey);
            writer.Write(BlowfishReceiveKey);
        }
    }
}
