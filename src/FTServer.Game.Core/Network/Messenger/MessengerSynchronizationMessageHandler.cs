using FTServer.Contracts.Network;
using FTServer.Network;
using FTServer.Network.Message.Messenger;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Messenger
{
    [NetworkMessageHandler(MessengerSynchronizationRequest.MessageId)]
    public class MessengerSynchronizationMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        public MessengerSynchronizationMessageHandler()
        {
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is MessengerSynchronizationRequest request)
            {
                await context.SendAsync(new MessengerSynchronizationResponse() { Type = request.Type });
            }
        }
    }
}
