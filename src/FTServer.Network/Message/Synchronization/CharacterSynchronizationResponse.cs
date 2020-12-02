using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Synchronization
{
    [NetworkMessage(MessageId)]
    public class CharacterSynchronizationResponse : NetworkMessage
    {
        public const ushort MessageId = 0x105F;

        public override int MaximumSize => 11;

        public CharacterSynchronizationRequest.SynchronizationType Type { get; set; }
        public bool Failure { get; set; }

        public CharacterSynchronizationResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)Type);
            writer.WriteUInt16((ushort)(Failure ? 1 : 0));
        }
    }
}
