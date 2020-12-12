using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Parcel
{
    [NetworkMessage(MessageId)]
    public class ParcelSynchronizationRequest : NetworkMessage
    {
        public const ushort MessageId = 0x219C;
        public override int MaximumSize => 8;

        public byte Type { get; set; }
        public int Unknown { get; set; }

        public ParcelSynchronizationRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Type = reader.ReadByte();
            Unknown = reader.ReadInt32();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
