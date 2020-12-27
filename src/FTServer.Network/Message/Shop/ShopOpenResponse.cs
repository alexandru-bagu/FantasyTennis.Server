using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopOpenResponse : NetworkMessage
    {
        public const ushort MessageId = 0x2383;

        public override int MaximumSize => 14;

        public bool Failure { get; set; }
        public int UnknownValue { get; set; }

        public ShopOpenResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt16((short)(Failure ? -1 : 0));
            writer.WriteInt32(UnknownValue);
        }
    }
}
