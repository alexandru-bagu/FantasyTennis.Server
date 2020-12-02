using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Authentication
{
    [NetworkMessage(MessageId)]
    public class ReauthenticationResponse : NetworkMessage
    {
        public const ushort MessageId = 0xFAA;
        public override int MaximumSize => 10;

        public bool Failure { get; set; }

        public ReauthenticationResponse() : base(MessageId)
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
        }
    }
}