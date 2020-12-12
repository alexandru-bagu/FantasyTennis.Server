using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Parcel
{
    [NetworkMessage(MessageId)]
    public class CoupleSynchronizationResponse : NetworkMessage
    {
        public const ushort MessageId = 0x2527;
        public override int MaximumSize => 4096;


        public CoupleSynchronizationResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(0);
            writer.WriteByte(0);
        }
    }
}
