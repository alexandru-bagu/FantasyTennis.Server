using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Network;
using FTServer.Network.Message.Synchronization;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network
{
    [NetworkMessageHandler(DisconnectRequest.MessageId)]
    public class DisconnectMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public DisconnectMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyMinimumState(GameState.Authenticate)) return;
            await using (var uow = _unitOfWorkFactory.Create())
            {
                uow.Attach(context.Character.Account);
                context.Character.Account.Online = false;
                await uow.CommitAsync();
            }
            await context.SendAsync(new DisconnectResponse());
        }
    }
}
