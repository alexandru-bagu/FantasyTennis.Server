using FTServer.Contracts.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTServer.Network.Message.Character
{
    [NetworkMessage(MessageId)]
    public class CheckCharacterNameResponse : NetworkMessage
    {
        public const ushort MessageId = 0x101A;

        public override int MaximumSize => 16;

        public bool Failure { get; set; }

        public CheckCharacterNameResponse() : base(MessageId)
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