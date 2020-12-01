using FTServer.Contracts.MemoryManagement;
using System;
using System.Text;

namespace FTServer.Network.Message.Character
{
    [NetworkMessage(MessageId)]
    public class CreateCharacterRequest : NetworkMessage
    {
        public const ushort MessageId = 0x101B;

        public override int MaximumSize => 32;

        public int CharacterId { get; set; }
        public string Name { get; set; }
        public byte Strength { get; set; }
        public byte Stamina { get; set; }
        public byte Dexterity { get; set; }
        public byte Willpower { get; set; }
        public byte StatusPoints { get; set; }
        public byte Level { get; set; }

        public CreateCharacterRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            CharacterId = reader.ReadInt32();
            Name = reader.ReadString(Encoding.Unicode);
            Strength = reader.ReadByte();
            Stamina = reader.ReadByte();
            Dexterity = reader.ReadByte();
            Willpower = reader.ReadByte();
            StatusPoints = reader.ReadByte();
            Level = reader.ReadByte();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}