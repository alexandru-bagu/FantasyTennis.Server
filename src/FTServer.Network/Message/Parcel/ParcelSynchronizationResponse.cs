using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Parcel
{
    [NetworkMessage(MessageId)]
    public class ParcelSynchronizationResponse : NetworkMessage
    {
        public const ushort MessageId = 0x219D;
        public override int MaximumSize => 4096;


        public ParcelSynchronizationResponse() : base(MessageId)
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
