using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.System.Session
{
    /// <summary>
    /// Message to initialize the blowfish crypto client-side. Serial key will default to -1 and so all messages will start with double 0.
    /// </summary>
    [NetworkMessage(MessageId)]
    public class BlowfishSessionHello : NetworkMessage
    {
        public const ushort MessageId = 0xFF99;
        public override int MaximumSize => 40;

        public byte[] BlowfishSendKey { get; set; }
        public byte[] BlowfishReceiveKey { get; set; }

        public BlowfishSessionHello() : base(MessageId)
        {
        }

        public BlowfishSessionHello(byte[] blowfishSendKey, byte[] blowishReceiveKey) : this()
        {
            BlowfishSendKey = blowfishSendKey;
            BlowfishReceiveKey = blowishReceiveKey;
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.Write(BlowfishSendKey);
            writer.Write(BlowfishReceiveKey);
        }
    }
}
