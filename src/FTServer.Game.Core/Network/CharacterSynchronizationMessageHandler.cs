using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Network;
using FTServer.Network.Message.Synchronization;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network
{
    [NetworkMessageHandler(CharacterSynchronizationRequest.MessageId)]
    public class CharacterSynchronizationMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CharacterSynchronizationMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyMinimumState(GameState.Authenticate)) return;
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
                        context.State = GameState.SynchronizeUnknown2;
                        break;
                    case CharacterSynchronizationRequest.SynchronizationType.Unknown2:
                        if (await context.FaultyState(GameState.SynchronizeUnknown2)) return;
                        context.State = GameState.Online;
                        break;
                }
                await context.SendAsync(new CharacterSynchronizationResponse() { Type = request.Type });
            }
        }
    }
}
