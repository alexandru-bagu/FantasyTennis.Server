using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Character
{
    [NetworkMessage(MessageId)]
    public class SynchronizeCurrencyResponse : NetworkMessage
    {
        public const ushort MessageId = 0x1B61;
        public override int MaximumSize => 8;

        public int Ap { get; set; }
        public int Gold { get; set; }

        public SynchronizeCurrencyResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt32(Ap);
            writer.WriteInt32(Gold);
        }
    }
}
