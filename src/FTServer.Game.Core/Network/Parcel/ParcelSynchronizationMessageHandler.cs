using FTServer.Contracts.Network;
using FTServer.Network;
using FTServer.Network.Message.Parcel;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Parcel
{
    [NetworkMessageHandler(ParcelSynchronizationRequest.MessageId)]
    public class ParcelSynchronizationMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        public ParcelSynchronizationMessageHandler()
        {
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is ParcelSynchronizationRequest request)
            {
                await context.SendAsync(new ParcelSynchronizationResponse());
            }
        }
    }
}
