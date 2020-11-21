using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message.System.Session
{
    /// <summary>
    /// Message to initialize the encryption-less protocol. Serial keys will default to -1 and so all messages will begin with double 0.
    /// </summary>
    [NetworkMessage(MessageId)]
    public class PlainSessionHello : NetworkMessage
    {
        public const ushort MessageId = 0xFF9B;
        public override int MaximumSize => 8;


        public PlainSessionHello() : base(MessageId)
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
