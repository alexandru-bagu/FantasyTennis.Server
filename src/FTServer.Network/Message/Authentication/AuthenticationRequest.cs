using FTServer.Contracts.MemoryManagement;
using System;
using System.Text;

namespace FTServer.Network.Message.Authentication
{
    [NetworkMessage(MessageId)]
    public class AuthenticationRequest : NetworkMessage
    {
        public const ushort MessageId = 0x0FA1;
        public override int MaximumSize => 40;

        public string Username { get; set; }
        public string Password { get; set; }
        public int ClientLongVersion { get; set; }
        public byte AuthenticationStage { get; set; }

        public AuthenticationRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Username = reader.ReadString(Encoding.Unicode);
            Password = reader.ReadString(Encoding.ASCII);
            if (reader.Length >= 5) ClientLongVersion = reader.ReadInt32();
            if (reader.Length >= 1) AuthenticationStage = reader.ReadByte();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
