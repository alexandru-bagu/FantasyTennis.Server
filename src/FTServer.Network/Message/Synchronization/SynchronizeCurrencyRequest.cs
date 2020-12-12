using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Synchronization
{
    [NetworkMessage(MessageId)]
    public class SynchronizeCurrencyRequest : NetworkMessage
    {
        public const ushort MessageId = 0x1B60;
        public override int MaximumSize => 8;

        public SynchronizeCurrencyRequest() : base(MessageId)
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
