using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Network;
using FTServer.Network.Message;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(ServerListRequest.MessageId)]
    public class ServerListMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ServerListMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (await context.FaultyState(AuthenticationState.Online)) return;
            List<GameServer> servers;
            await using (var uow =  _unitOfWorkFactory.Create())
                servers = await uow.GameServers.Where(p => p.Enabled).ToListAsync();
            await context.SendAsync(new ServerListResponse() { GameServers = servers });
        }
    }
}
