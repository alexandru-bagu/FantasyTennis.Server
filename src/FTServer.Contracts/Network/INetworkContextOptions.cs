using System.IO;
using System.Net;

namespace FTServer.Contracts.Network
{
    public interface INetworkContextOptions
    {
        Stream Stream { get; }
        EndPoint RemoteEndPoint { get; }
        string RemoteIPAddress { get; }
        EndPoint LocalEndPoint { get; }
        string LocalIPAddress { get; }
    }
}
