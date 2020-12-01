using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Character
{
    [NetworkMessage(MessageId)]
    public class ReserveCharacterRequest : NetworkMessage
    {
        public const ushort MessageId = 0x101E;

        public override int MaximumSize => 9;

        public CharacterType Type { get; set; }

        public ReserveCharacterRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Type = (CharacterType)reader.ReadByte();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
