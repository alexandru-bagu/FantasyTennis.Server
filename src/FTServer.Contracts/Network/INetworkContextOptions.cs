using System.IO;
using System.Net;

namespace FTServer.Contracts.Network
{
    public interface INetworkContextOptions
    {
        Stream Stream { get; }
        EndPoint RemoteEndPoint { get; }
        EndPoint LocalEndPoint { get; }
    }
}
