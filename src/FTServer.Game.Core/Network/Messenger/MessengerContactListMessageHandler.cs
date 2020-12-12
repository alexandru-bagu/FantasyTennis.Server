using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Network;
using FTServer.Network.Message.Messenger;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Messenger
{
    [NetworkMessageHandler(MessengerContactListRequest.MessageId)]
    public class MessengerContactListMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public MessengerContactListMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is MessengerContactListRequest request)
            {
                await using (var uow = await _unitOfWorkFactory.Create())
                {
                    context.Friends = await uow.GetFriendships(context.Character.Id);
                    await context.SendAsync(new MessengerContactListResponse() { Friends = context.Friends });
                }
            }
        }
    }
}
