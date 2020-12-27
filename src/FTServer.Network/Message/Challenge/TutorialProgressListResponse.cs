using FTServer.Contracts.MemoryManagement;
using FTServer.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Network.Message.Tutorial
{
    public class TutorialProgressListResponse : NetworkMessage
    {
        public const ushort MessageId = 0x2210;

        public override int MaximumSize => 4096;
        public IEnumerable<TutorialProgress> Tutorials { get; set; }

        public TutorialProgressListResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUInt16((ushort)Tutorials.Count());
            foreach(var tutorial in Tutorials)
            {
                writer.WriteUInt16(tutorial.TutorialId);
                writer.WriteUInt16((ushort)tutorial.Completed);
                writer.WriteUInt16((ushort)tutorial.Attempts);
            }
        }
    }
}
