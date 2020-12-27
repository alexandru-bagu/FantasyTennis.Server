using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Network;
using FTServer.Network.Message.Tutorial;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Tutorial
{
    [NetworkMessageHandler(TutorialStartRequest.MessageId)]
    public class TutorialStartMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public TutorialStartMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is TutorialStartRequest tutorialStart)
            {
                await using (var uow = _unitOfWorkFactory.Create())
                {
                    var tutorial = await uow.TutorialProgress.Where(p => p.CharacterId == context.Character.Id && p.TutorialId == tutorialStart.TutorialId).FirstOrDefaultAsync();
                    if (tutorial == null)
                    {
                        tutorial = new TutorialProgress()
                        {
                            CharacterId = context.Character.Id,
                            TutorialId = tutorialStart.TutorialId
                        };
                        uow.TutorialProgress.Add(tutorial);
                    }
                    tutorial.Attempts++;
                    await uow.CommitAsync();
                }
            }
        }
    }
}
