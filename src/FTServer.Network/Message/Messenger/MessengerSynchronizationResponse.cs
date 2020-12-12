using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Messenger
{
    [NetworkMessage(MessageId)]
    public class MessengerSynchronizationResponse : NetworkMessage
    {
        public const ushort MessageId = 0x1F64;
        public override int MaximumSize => 4096;

        public MessengerType Type { get; set; } 

        public MessengerSynchronizationResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)Type);
            writer.WriteByte(0);
        }
    }
}
