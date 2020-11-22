using System;

namespace FTServer.Network
{
    public class NetworkMessageHandlerAttribute : Attribute
    {
        public ushort MessageId { get; }
        public NetworkMessageHandlerAttribute(ushort id)
        {
            MessageId = id;
        }
    }
}
