using System;

namespace FTServer.Network
{
    public class NetworkMessageAttribute : Attribute
    {
        public ushort MessageId { get; }
        public NetworkMessageAttribute(ushort id)
        {
            MessageId = id;
        }
    }
}
