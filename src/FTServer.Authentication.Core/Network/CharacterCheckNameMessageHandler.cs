using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Network;
using FTServer.Network.Message.Character;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(CheckCharacterNameRequest.MessageId)]
    public class CharacterCheckNameMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly ILogger<DefaultNetworkMessageHandler> _logger;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CharacterCheckNameMessageHandler(ILogger<DefaultNetworkMessageHandler> logger, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
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
