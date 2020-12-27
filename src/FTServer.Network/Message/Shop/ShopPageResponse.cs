using FTServer.Contracts.MemoryManagement;
using FTServer.Resources;
using System;
using System.Collections.Generic;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopPageResponse : NetworkMessage
    {
        public const ushort MessageId = 0x2388;

        public override int MaximumSize => 8;

        public int PageCount { get; set; }
        
        public List<ShopItem> Items { get; set; }

        public ShopPageResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt32(PageCount);
            writer.WriteUInt16((ushort)Items.Count);

            foreach(var item in Items)
            {
                writer.WriteInt32(item.Index);
                writer.WriteByte(item.PriceType);
                writer.WriteInt32(item.GoldBack);
                writer.WriteInt32(item.Use0);
                writer.WriteInt32(item.Use1);
                writer.WriteInt32(item.Use2);
                writer.WriteInt32(item.Price0);
                writer.WriteInt32(item.Price1);
                writer.WriteInt32(item.Price2);
                writer.WriteInt32(item.OldPrice0);
                writer.WriteInt32(item.OldPrice1);
                writer.WriteInt32(item.OldPrice2);
            }
        }
    }
}
