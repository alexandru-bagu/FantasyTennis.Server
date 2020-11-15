using System;

namespace FTServer.Network
{
    public abstract class NetworkMessageHandlerAttribute : Attribute
    {
        public ushort MessageId { get; }
        public NetworkMessageHandlerAttribute(ushort id)
        {
            MessageId = id;
        }
    }
}
