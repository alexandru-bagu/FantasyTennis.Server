﻿using FTServer.Contracts.MemoryManagement;
using FTServer.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTServer.Network.Message
{
    [NetworkMessage(MessageId)]
    public class ServerListResponse : NetworkMessage
    {
        public const ushort MessageId = 0x1010;
        private readonly IEnumerable<GameServer> _gameServers;

        public override int MaximumSize => 4096;

        public ServerListResponse() : base(MessageId)
        {
        }

        public ServerListResponse(IEnumerable<GameServer> gameServers) : this()
        {
            _gameServers = gameServers;
        }

        public override void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException();
        }

        public override void Serialize(IUnmanagedMemoryWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte((byte)_gameServers.Count());

            foreach(var server in _gameServers)
            {
                writer.WriteByte(server.UnknownByte);
                writer.WriteUInt16(server.Id);
                writer.WriteByte((byte)server.Type);
                writer.WriteString(server.Host, Encoding.Unicode, 256);
                writer.WriteUInt16(server.Port);
                writer.WriteUInt16(server.OnlineCount);
                writer.WriteBoolean(server.ShowName);
                if (server.ShowName) writer.WriteString(server.Name, Encoding.Unicode, 64);
            }
        }
    }
}
