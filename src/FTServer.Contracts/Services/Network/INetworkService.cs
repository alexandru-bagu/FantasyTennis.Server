using System;
using System.Net;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Network
{
    public interface INetworkService<T> : IDisposable, IAsyncDisposable
        where T : NetworkContext
    {
        IPEndPoint EndPoint { get; }
        Task ListenAsync(int backlog = 100);
        Task StopAsync();
    }
}
