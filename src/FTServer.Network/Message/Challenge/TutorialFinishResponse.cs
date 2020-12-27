using FTServer.Contracts.MemoryManagement;
using FTServer.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Network.Message.Tutorial
{
    public class TutorialFinishResponse : NetworkMessage
    {
        public const ushort MessageId = 0x2211;

        public override int MaximumSize => 4096;

        public bool Win { get; set; }
        public byte Level { get; set; }
        public int Experience { get; set; }
        public int EllapsedSeconds { get; set; }
        public List<Item> ItemReward { get; set; }

        public TutorialFinishResponse() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(Win);
            writer.WriteByte(Level);
            writer.WriteInt32(Experience);
            writer.WriteInt32(EllapsedSeconds);
            writer.WriteByte((byte)ItemReward.Count());
            foreach (var item in ItemReward)
                writer.WriteItem(item);
        }
    }
}
