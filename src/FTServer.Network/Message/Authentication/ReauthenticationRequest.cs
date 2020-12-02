using FTServer.Contracts.MemoryManagement;
using System;
using System.Text;

namespace FTServer.Network.Message.Authentication
{
    [NetworkMessage(MessageId)]
    public class ReauthenticationRequest : NetworkMessage
    {
        public const ushort MessageId = 0xFA9;
        public override int MaximumSize => 40;

        public string Username { get; set; }
        public AccountData Data { get; set; }
        public byte AuthenticationStage { get; set; }

        public ReauthenticationRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            var data = Data;
            base.Deserialize(reader);
            Username = reader.ReadString(Encoding.Unicode);
            data.AccountId = reader.ReadInt32();
            data.Unknown2 = reader.ReadInt32();
            data.Unknown3 = reader.ReadInt32();
            data.Unknown4 = reader.ReadInt32();
            data.Key1 = reader.ReadInt32();
            data.Key2 = reader.ReadInt32();
            Data = data;
            AuthenticationStage = reader.ReadByte();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}