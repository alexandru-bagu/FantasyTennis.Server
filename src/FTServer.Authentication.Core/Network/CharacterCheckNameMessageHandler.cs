using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Network;
using FTServer.Network.Message.Character;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(CheckCharacterNameRequest.MessageId)]
    public class CharacterCheckNameMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CharacterCheckNameMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (await context.FaultyState(AuthenticationState.Online)) return;
            if (message is CheckCharacterNameRequest checkName)
            {
                int count = 0;
                await using (var uow = await _unitOfWorkFactory.Create())
                    count = await uow.Characters.Where(p => p.Name == checkName.Name).CountAsync();
                await context.SendAsync(new CheckCharacterNameResponse() { Failure = count != 0 });
            }
        }
    }
}
