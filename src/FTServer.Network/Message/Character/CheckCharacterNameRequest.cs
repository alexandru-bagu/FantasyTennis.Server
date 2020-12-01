using FTServer.Contracts.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace FTServer.Network.Message.Character
{
    [NetworkMessage(MessageId)]
    public class CheckCharacterNameRequest : NetworkMessage
    {
        public const ushort MessageId = 0x1019;

        public override int MaximumSize => 32;

        public string Name { get; set; }

        public CheckCharacterNameRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Name = reader.ReadString(Encoding.Unicode);
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}