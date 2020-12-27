using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopOpenRequest : NetworkMessage
    {
        public const ushort MessageId = 0x2382;

        public override int MaximumSize => 8;

        public ShopOpenRequest() : base(MessageId)
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
