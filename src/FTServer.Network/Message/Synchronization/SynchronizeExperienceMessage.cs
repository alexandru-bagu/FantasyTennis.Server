using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Synchronization
{
    [NetworkMessage(MessageId)]
    public class SynchronizeExperienceMessage : NetworkMessage
    {
        public const ushort MessageId = 0x22B8;

        public override int MaximumSize => 9;

        public byte Level { get; set; }
        public int Experience { get; set; }

        public SynchronizeExperienceMessage() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(Level);
            writer.WriteInt32(Experience);
        }
    }
}
