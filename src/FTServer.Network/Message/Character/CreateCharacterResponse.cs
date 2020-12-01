using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Character
{
    [NetworkMessage(MessageId)]
    public class CreateCharacterResponse : NetworkMessage
    {
        public const ushort MessageId = 0x101C;

        public override int MaximumSize => 32;

        public bool Failure { get; set; }

        public CreateCharacterResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUInt16((ushort)(Failure ? 1 : 0));
        }
    }
}