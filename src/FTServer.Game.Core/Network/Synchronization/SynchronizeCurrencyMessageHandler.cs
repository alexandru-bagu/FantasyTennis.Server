using FTServer.Contracts.Network;
using FTServer.Network;
using FTServer.Network.Message.Character;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Synchronization
{
    [NetworkMessageHandler(SynchronizeCurrencyRequest.MessageId)]
    public class SynchronizeCurrencyMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        public SynchronizeCurrencyMessageHandler()
        {
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            await context.SendAsync(new SynchronizeCurrencyResponse()
            {
                Ap = context.Character.Account.Ap,
                Gold = context.Character.Gold
            });
        }
    }
}
