using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Character
{
    [NetworkMessage(MessageId)]
    public class ReserveCharacterResponse : NetworkMessage
    {
        public const ushort MessageId = 0x101F;

        public override int MaximumSize => 9;

        public bool Failure => CharacterId == 0;
        public int CharacterId { get; set; }
        public CharacterType Type { get; set; }

        public ReserveCharacterResponse() : base(MessageId)
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
            if (CharacterId > 0)
            {
                writer.WriteInt32(CharacterId);
                writer.WriteByte((byte)Type);
            }
        }
    }
}
