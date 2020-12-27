using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.System
{
    [NetworkMessage(MessageId)]
    public class DisconnectResponse : NetworkMessage
    {
        public const ushort MessageId = 0x0FA8;

        public override int MaximumSize => 8;

        public DisconnectResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
        }
    }
}
