using FTServer.Contracts.Network;
using System.IO;
using System.Net;

namespace FTServer.Network
{
    public class NetworkContextOptions : INetworkContextOptions
    {
        public Stream Stream { get; protected set; }
        public EndPoint RemoteEndPoint { get; protected set; }
        public EndPoint LocalEndPoint { get; protected set; }

        public NetworkContextOptions(Stream stream, EndPoint remoteEndPoint, EndPoint localEndPoint)
        {
            Stream = stream;
            RemoteEndPoint = remoteEndPoint;
            LocalEndPoint = localEndPoint;
        }
    }
}
