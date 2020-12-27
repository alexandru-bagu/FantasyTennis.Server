using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopPageRequest : NetworkMessage
    {
        public const ushort MessageId = 0x2387;

        public override int MaximumSize => 11;

        public int Category { get; set; }
        public int Part { get; set; }
        public int Hero { get; set; }
        public int Page { get; set; }

        public ShopPageRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Category = reader.ReadSByte();
            Part = reader.ReadSByte();
            Hero = reader.ReadSByte();
            Page = reader.ReadInt32();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
