using FTServer.Contracts.MemoryManagement;
using System;
using System.Text;

namespace FTServer.Network.Message.Authentication
{
    [NetworkMessage(MessageId)]
    public class GameAuthenticationRequest : NetworkMessage
    {
        public const ushort MessageId = 0x1069;
        public override int MaximumSize => 40;

        public int CharacterId { get; set; }
        public AccountData Data { get; set; }
        public string Username { get; set; }
        public byte AuthenticationStage { get; set; }

        public GameAuthenticationRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            var data = Data;
            base.Deserialize(reader);
            CharacterId = reader.ReadInt32();
            data.AccountId = reader.ReadInt32();
            data.Unknown2 = reader.ReadInt32();
            data.Unknown3 = reader.ReadInt32();
            data.Unknown4 = reader.ReadInt32();
            data.Key1 = reader.ReadInt32();
            data.Key2 = reader.ReadInt32();
            Data = data;
            Username = reader.ReadString(Encoding.Unicode);
            AuthenticationStage = reader.ReadByte();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}