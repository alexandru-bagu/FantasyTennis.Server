using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopItemCountResponse : NetworkMessage
    {
        public const ushort MessageId = 0x238A;

        public override int MaximumSize => 8;

        public ItemCategoryType Category { get; set; }
        public ItemPartType Part { get; set; }
        public HeroType Hero { get; set; }
        public int Count { get; set; }

        public ShopItemCountResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(Category);
            writer.WriteByte(Part);
            writer.WriteByte(Hero);
            writer.WriteInt32(Count);
        }
    }
}
