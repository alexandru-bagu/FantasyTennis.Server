using FTServer.Contracts.MemoryManagement;
using System;
using System.Text;

namespace FTServer.Network.Message.Authentication
{
    [NetworkMessage(MessageId)]
    public class GameAuthenticationResponse : NetworkMessage
    {
        public const ushort MessageId = 0x106A;
        public override int MaximumSize => 11;

        public bool Failure { get; set; }
        public byte Flag { get; set; }

        public GameAuthenticationResponse() : base(MessageId)
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
            writer.WriteByte(Flag);
        }
    }
}