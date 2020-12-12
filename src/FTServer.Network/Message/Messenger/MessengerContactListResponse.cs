using FTServer.Contracts.MemoryManagement;
using FTServer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTServer.Network.Message.Messenger
{
    [NetworkMessage(MessageId)]
    public class MessengerContactListResponse : NetworkMessage
    {
        public const ushort MessageId = 0x1F4A;

        public override int MaximumSize => 4096;

        public IEnumerable<FriendDto> Friends { get; set; }

        public MessengerContactListResponse() : base(MessageId)
        {
            Friends = new List<FriendDto>();
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)Friends.Count());
            foreach (var friend in Friends)
            {
                writer.WriteInt32(friend.Id);
                writer.WriteString(friend.Name, Encoding.Unicode, 12);
                writer.WriteByte(friend.Type);
                writer.WriteInt16(friend.ActiveServer);
            }
        }
    }
}
