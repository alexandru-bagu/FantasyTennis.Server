using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopItemCountRequest : NetworkMessage
    {
        public const ushort MessageId = 0x2389;

        public override int MaximumSize => 8;

        public int Category { get; set; }
        public int Part { get; set; }
        public int Hero { get; set; }

        public ShopItemCountRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Category = reader.ReadSByte();
            Part = reader.ReadSByte();
            Hero = reader.ReadSByte();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
