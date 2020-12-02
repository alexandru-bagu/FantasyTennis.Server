using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Synchronization
{
    [NetworkMessage(MessageId)]
    public class CharacterSynchronizationRequest : NetworkMessage
    {
        public const ushort MessageId = 0x105E;

        public override int MaximumSize => 9;

        public enum SynchronizationType
        {
            Experience = 0,
            Home = 1,
            Inventory = 2,
            Unknown = 3,
            Unknown2 = 4
        }

        public SynchronizationType Type { get; set; }

        public CharacterSynchronizationRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Type = (SynchronizationType)reader.ReadByte();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
