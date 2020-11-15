using FTServer.Contracts.Network;
using System;
using System.Net;
using System.Threading.Tasks;

namespace FTServer.Contracts.Services.Network
{
    public interface INetworkService<T> : IDisposable, IAsyncDisposable
        where T : INetworkContext
    {
        IPEndPoint EndPoint { get; }
        Task ListenAsync(int backlog = 100);
        Task StopAsync();
    }
}
