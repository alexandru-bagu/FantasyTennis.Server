using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopPurchaseRequest : NetworkMessage
    {
        public const ushort MessageId = 0x1B67;

        public override int MaximumSize => 128;

        public int Amount { get; private set; }

        public PurchaseItem[] Items { get; private set; }

        public ShopPurchaseRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Amount = reader.ReadByte();
            Items = new PurchaseItem[Amount];
            for (int i = 0; i < Amount; i++)
            {
                var item = Items[i] = new PurchaseItem();
                item.Index = reader.ReadInt32();
                item.Option = reader.ReadByte();
            }
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }

        public class PurchaseItem
        {
            public int Index { get; set; }
            public byte Option { get; set; }
        }
    }
}
