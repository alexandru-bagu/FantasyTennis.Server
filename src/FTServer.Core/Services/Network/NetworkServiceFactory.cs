using FTServer.Contracts.Services.Network;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FTServer.Core.Services.Network
{
    public class NetworkServiceFactory : INetworkServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public NetworkServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public INetworkService<T> Create<T>(string host, int port) where T : NetworkContext
        {
            return (INetworkService<T>)ActivatorUtilities.CreateInstance(_serviceProvider, typeof(NetworkService<T>), host, port);
        }
    }
}
