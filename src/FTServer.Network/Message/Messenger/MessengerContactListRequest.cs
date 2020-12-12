using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Messenger
{
    [NetworkMessage(MessageId)]
    public class MessengerContactListRequest : NetworkMessage
    {
        public const ushort MessageId = 0x1F49;

        public override int MaximumSize => 8;

        public MessengerContactListRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
