using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Tutorial
{
    [NetworkMessage(MessageId)]
    public class TutorialProgressListRequest : NetworkMessage
    {
        public const ushort MessageId = 0x220F;

        public override int MaximumSize => 8;

        public TutorialProgressListRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
