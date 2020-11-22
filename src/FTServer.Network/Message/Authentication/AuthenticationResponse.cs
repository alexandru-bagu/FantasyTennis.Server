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
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }
        public int Unknown3 { get; set; }
        public int Unknown4 { get; set; }
        public int Unknown5 { get; set; }
        public int Unknown6 { get; set; }

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
                writer.Write(Unknown1);
                writer.Write(Unknown2);
                writer.Write(Unknown3);
                writer.Write(Unknown4);
                writer.Write(Unknown5);
                writer.Write(Unknown6);
            }
        }
    }
}
