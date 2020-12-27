using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Shop
{
    [NetworkMessage(MessageId)]
    public class ShopItemCountResponse : NetworkMessage
    {
        public const ushort MessageId = 0x238A;

        public override int MaximumSize => 16;

        public int Category { get; set; }
        public int Part { get; set; }
        public int Hero { get; set; }
        public int Pages { get; set; }

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
            writer.WriteSByte((sbyte)Category);
            writer.WriteSByte((sbyte)Part);
            writer.WriteSByte((sbyte)Hero);
            writer.WriteInt32(Pages);
        }
    }
}
