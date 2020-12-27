using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Network;
using FTServer.Network.Message.Tutorial;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Tutorial
{
    [NetworkMessageHandler(TutorialProgressListRequest.MessageId)]
    public class TutorialProgressListMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public TutorialProgressListMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            await using (var uow = _unitOfWorkFactory.Create())
            {
                var list = await uow.TutorialProgress.Where(p => p.CharacterId == context.Character.Id).ToListAsync();
                await context.SendAsync(new TutorialProgressListResponse() { Tutorials = list });
            }
        }
    }
}
