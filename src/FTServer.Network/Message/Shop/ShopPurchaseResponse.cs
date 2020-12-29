using FTServer.Contracts.MemoryManagement;
using FTServer.Database.Model;
using System;
using System.Collections.Generic;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopPurchaseResponse : NetworkMessage
    {
        public const ushort MessageId = 0x1B68;

        public override int MaximumSize => 396;

        public short Result { get; set; }
        public List<Item> Items { get; set; }

        public ShopPurchaseResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt16(Result);
            writer.WriteUInt16((ushort)Items.Count);

            foreach (var item in Items)
            {
                writer.WriteItem(item);
            }
        }
    }
}
