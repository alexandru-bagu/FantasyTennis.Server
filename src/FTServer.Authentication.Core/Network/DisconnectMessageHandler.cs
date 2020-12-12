using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Network;
using FTServer.Network.Message.Synchronization;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(DisconnectRequest.MessageId)]
    public class DisconnectMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public DisconnectMessageHandler( IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (await context.FaultyState(AuthenticationState.Online)) return;
            context.State = AuthenticationState.Offline;
            await using (var uow = _unitOfWorkFactory.Create())
            {
                uow.Attach(context.Account);
                context.Account.Online = false;
                await uow.CommitAsync();
            }
            await context.SendAsync(new DisconnectResponse());
        }
    }
}
