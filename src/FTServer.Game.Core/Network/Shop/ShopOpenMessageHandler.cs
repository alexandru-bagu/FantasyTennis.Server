using FTServer.Contracts.Network;
using FTServer.Network;
using FTServer.Network.Message.Shop;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Shop
{
    [NetworkMessageHandler(ShopOpenRequest.MessageId)]
    public class ShopOpenMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            await context.SendAsync(new ShopOpenResponse() { Failure = false, UnknownValue = 0 });
        }
    }
}
