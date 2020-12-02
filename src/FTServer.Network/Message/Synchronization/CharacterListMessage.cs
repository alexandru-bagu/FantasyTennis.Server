using FTServer.Contracts.MemoryManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTServer.Network.Message.Synchronization
{
    [NetworkMessage(MessageId)]
    public class CharacterListMessage : NetworkMessage
    {
        public const ushort MessageId = 0x1005;

        public override int MaximumSize => 4096;

        /// <summary>
        /// CCharacterWrapper::CClientNet_Vftable
        /// </summary>
        public int CClientNet_Vftable { get; set; }
        /// <summary>
        /// CClientNet::Unknown_ProcessCharacterList_Dword1
        /// </summary>
        public int Unknown2 { get; set; }
        /// <summary>
        /// CClientNet::Unknown_ProcessCharacterList_Byte1
        /// </summary>
        public byte Unknown3 { get; set; }
        /// <summary>
        /// CClientNet::Unknown_ProcessCharacterList_Byte2
        /// </summary>
        public byte Unknown4 { get; set; }
        public int SelectedCharacterId { get; set; }
        public IEnumerable<Database.Model.Character> Characters { get; set; }

        public CharacterListMessage() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);

            writer.WriteInt32(CClientNet_Vftable);
            writer.WriteInt32(Unknown2);
            writer.WriteByte(Unknown3);
            writer.WriteInt32(SelectedCharacterId);
            writer.WriteByte(Unknown4);
            writer.WriteByte((byte)Characters.Count());

            foreach (var character in Characters)
            {
                writer.WriteInt32(character.Id);
                writer.WriteString(character.Name, Encoding.Unicode, 12);
                writer.WriteByte(character.Level);
                writer.WriteBoolean(character.IsCreated);
                writer.WriteByte(character.UnknownByte1 = 1);
                writer.WriteInt32(character.Gold);
                writer.WriteByte((byte)character.Type);
                writer.WriteByte(character.Strength);
                writer.WriteByte(character.Stamina);
                writer.WriteByte(character.Dexterity);
                writer.WriteByte(character.Willpower);
                writer.WriteByte(character.StatusPoints);
                writer.WriteBoolean(character.NameChangeAllowed && !character.NameChangeByIcon);
                writer.WriteBoolean(character.NameChangeAllowed && character.NameChangeByIcon);
                writer.Write(character.HairId);
                writer.Write(character.FaceId);
                writer.Write(character.DressId);
                writer.Write(character.PantsId);
                writer.Write(character.SocksId);
                writer.Write(character.ShoesId);
                writer.Write(character.GlovesId);
                writer.Write(character.RacketId);
                writer.Write(character.GlassesId);
                writer.Write(character.BagId);
                writer.Write(character.HatId);
                writer.Write(character.DyeId);
            }
        }
    }
}
