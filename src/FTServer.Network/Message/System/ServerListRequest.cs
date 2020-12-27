using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.System
{
    [NetworkMessage(MessageId)]
    public class ServerListRequest : NetworkMessage
    {
        public const ushort MessageId = 0x100F;
        public override int MaximumSize => 8;

        public ServerListRequest() : base(MessageId)
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
