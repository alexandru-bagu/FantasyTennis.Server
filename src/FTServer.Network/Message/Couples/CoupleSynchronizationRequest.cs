using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Parcel
{
    [NetworkMessage(MessageId)]
    public class CoupleSynchronizationRequest : NetworkMessage
    {
        public const ushort MessageId = 0x2526;
        public override int MaximumSize => 8;

        public byte Type { get; set; }
        public int Unknown { get; set; }

        public CoupleSynchronizationRequest() : base(MessageId)
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
