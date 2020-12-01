using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Authentication
{
    [NetworkMessage(MessageId)]
    public class AuthenticationResponse : NetworkMessage
    {
        public const ushort MessageId = 0x0FA2;
        public override int MaximumSize => 40;

        public AuthenticationResult Result { get; set; }
        public AccountData Data { get; set; }

        public AuthenticationResponse() : base(MessageId)
        {
        }

        public AuthenticationResponse(AuthenticationResult result) : this()
        {
            Result = result;
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.Write((short)Result);
            if (Result == AuthenticationResult.Success)
            {
                writer.Write(Data.AccountId);
                writer.Write(Data.Unknown2);
                writer.Write(Data.Unknown3);
                writer.Write(Data.Unknown4);
                writer.Write(Data.Key1);
                writer.Write(Data.Key2);
            }
        }
    }
}
