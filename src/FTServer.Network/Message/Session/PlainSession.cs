using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message.Login
{
    [NetworkMessage(MessageId)]
    public class PlainSession : NetworkMessage
    {
        public const ushort MessageId = 0xFF9B;
        public override int MaximumSize => 8;


        public PlainSession() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
        }
    }
}
