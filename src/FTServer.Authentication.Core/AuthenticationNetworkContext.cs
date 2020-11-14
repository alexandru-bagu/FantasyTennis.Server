using FTServer.Contracts.MemoryManagement;
using FTServer.Contracts.Security;
using FTServer.Contracts.Services.Network;
using FTServer.Network.Message.Login;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core
{
    public class AuthenticationNetworkContext : NetworkContext
    {
        private readonly IUnmanagedMemoryService _unmanagedMemoryService;

        public AuthenticationNetworkContext(Stream connection, ICryptographicServiceFactory cryptographicServiceFactory, IUnmanagedMemoryService unmanagedMemoryService) : base(connection, cryptographicServiceFactory)
        {
            _unmanagedMemoryService = unmanagedMemoryService;
        }

        protected override Task Connected()
        {
            SendAsync(new WelcomeMessage(1, 2, 3, 4));
            return Task.CompletedTask;
        }

        protected override Task Disconnected()
        {
            return Task.CompletedTask;
        }
    }
}
