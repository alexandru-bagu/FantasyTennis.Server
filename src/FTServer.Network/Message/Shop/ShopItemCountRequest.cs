using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopItemCountRequest : NetworkMessage
    {
        public const ushort MessageId = 0x2389;

        public override int MaximumSize => 8;

        public ItemCategoryType Category { get; set; }
        public ItemPartType Part { get; set; }
        public HeroType Hero { get; set; }

        public ShopItemCountRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Category = reader.ReadByte();
            Part = reader.ReadByte();
            Hero = reader.ReadByte();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
