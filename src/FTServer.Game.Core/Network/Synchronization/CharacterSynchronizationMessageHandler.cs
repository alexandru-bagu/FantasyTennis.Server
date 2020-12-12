using FTServer.Contracts.Network;
using FTServer.Network;
using FTServer.Network.Message.Synchronization;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Synchronization
{
    [NetworkMessageHandler(CharacterSynchronizationRequest.MessageId)]
    public class CharacterSynchronizationMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        public CharacterSynchronizationMessageHandler()
        {
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyMinimumState(GameState.SynchronizeExperience)) return;
            if (message is CharacterSynchronizationRequest request)
            {
                switch (request.Type)
                {
                    case CharacterSynchronizationRequest.SynchronizationType.Experience:
                        if (await context.FaultyState(GameState.SynchronizeExperience)) return;
                        context.State = GameState.SynchronizeHome;
                        await context.SendAsync(new SynchronizeExperienceMessage() { Experience = context.Character.Experience, Level = context.Character.Level });
                        break;
                    case CharacterSynchronizationRequest.SynchronizationType.Home:
                        if (await context.FaultyState(GameState.SynchronizeHome)) return;
                        context.State = GameState.SynchronizeInventory;
                        break;
                    case CharacterSynchronizationRequest.SynchronizationType.Inventory:
                        if (await context.FaultyState(GameState.SynchronizeInventory)) return;
                        context.State = GameState.SynchronizeUnknown1;
                        break;
                    case CharacterSynchronizationRequest.SynchronizationType.Unknown:
                        if (await context.FaultyState(GameState.SynchronizeUnknown1)) return;
                        context.State = GameState.SynchronizeCoupleSystem;
                        break;
                    case CharacterSynchronizationRequest.SynchronizationType.CoupleSystem:
                        if (await context.FaultyState(GameState.SynchronizeCoupleSystem)) return;
                        context.State = GameState.Online;
                        break;
                }
                await context.SendAsync(new CharacterSynchronizationResponse() { Type = request.Type });
            }
        }
    }
}
