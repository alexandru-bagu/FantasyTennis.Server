﻿using FTServer.Contracts.MemoryManagement;

namespace FTServer.Network.Message
{
    public class UnknownNetworkMessage : NetworkMessage
    {
        public override int MaximumSize => 4096;
        public byte[] Body { get; set; }

        public UnknownNetworkMessage() : base(0)
        {
            Body = new byte[0];
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            Body = reader.ReadBytes(Header.BodyLength);
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBytes(Body);
        }
    }
}
