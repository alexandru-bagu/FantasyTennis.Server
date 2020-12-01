using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Synchronization
{
    [NetworkMessage(MessageId)]
    public class DisconnectRequest : NetworkMessage
    {
        public const ushort MessageId = 0x0FA7;

        public override int MaximumSize => 8;

        public DisconnectRequest() : base(MessageId)
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
