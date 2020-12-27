using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopPageRequest : NetworkMessage
    {
        public const ushort MessageId = 0x2387;

        public override int MaximumSize => 11;

        public ItemCategoryType Category { get; set; }
        public ItemPartType Part { get; set; }
        public HeroType Hero { get; set; }
        public int Page { get; set; }

        public ShopPageRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Category = reader.ReadByte();
            Part = reader.ReadByte();
            Hero = reader.ReadByte();
            Page = reader.ReadInt32();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
