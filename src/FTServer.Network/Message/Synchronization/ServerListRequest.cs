using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message
{
    [NetworkMessage(MessageId)]
    public class ServerListRequest : NetworkMessage
    {
        public const ushort MessageId = 0x100F;
        public override int MaximumSize => 4096;
        public byte[] Body { get; set; }

        public ServerListRequest() : base(MessageId)
        {
            Body = new byte[0];
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Body = reader.ReadBytes(Header.BodyLength);
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBytes(Body);
        }
    }
}
