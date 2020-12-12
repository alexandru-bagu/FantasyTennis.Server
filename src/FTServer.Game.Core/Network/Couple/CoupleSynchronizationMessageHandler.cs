using FTServer.Contracts.Network;
using FTServer.Network;
using FTServer.Network.Message.Parcel;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Couple
{
    [NetworkMessageHandler(CoupleSynchronizationRequest.MessageId)]
    public class CoupleSynchronizationMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        public CoupleSynchronizationMessageHandler()
        {
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is CoupleSynchronizationRequest request)
            {
                await context.SendAsync(new CoupleSynchronizationResponse());
            }
        }
    }
}
