using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Messenger
{
    [NetworkMessage(MessageId)]
    public class MessengerSynchronizationRequest : NetworkMessage
    {
        public const ushort MessageId = 0x1F63;
        public override int MaximumSize => 8;

        public MessengerType Type { get; set; }
        public int Unknown { get; set; }

        public MessengerSynchronizationRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Type = (MessengerType)reader.ReadByte();
            Unknown = reader.ReadInt32();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
