﻿using FTServer.Contracts.MemoryManagement;
using System;

namespace FTServer.Network.Message.Tutorial
{
    [NetworkMessage(MessageId)]
    public class TutorialStartRequest : NetworkMessage
    {
        public const ushort MessageId = 0x220D;

        public override int MaximumSize => 8;

        public ushort TutorialId { get; set; }

        public TutorialStartRequest() : base(MessageId)
        {
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            base.Deserialize(reader);
            TutorialId = reader.ReadUInt16();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
