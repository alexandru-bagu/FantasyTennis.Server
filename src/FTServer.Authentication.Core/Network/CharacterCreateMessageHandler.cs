using FTServer.Contracts.Game;
using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Network;
using FTServer.Network.Message.Character;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(CreateCharacterRequest.MessageId)]
    public class CharacterCreateMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ICharacterStatValidationService _characterStatValidationService;

        public CharacterCreateMessageHandler(IUnitOfWorkFactory unitOfWorkFactory, ICharacterStatValidationService characterStatValidationService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _characterStatValidationService = characterStatValidationService;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (await context.FaultyState(AuthenticationState.Online)) return;
            var success = await CreateCharacter(message, context);
            await context.SendAsync(new CreateCharacterResponse() { Failure = !success });
        }

        private async Task<bool> CreateCharacter(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (message is CreateCharacterRequest msg)
            {
                int count = 0;
                await using (var uow = _unitOfWorkFactory.Create())
                {
                    count = await uow.Characters.Where(p => p.Name == msg.Name).CountAsync();
                    if (count == 1)
                    {
                        return false;
                    }
                    else
                    {
                        var character = await uow.Characters.Where(p => p.Id == msg.CharacterId).FirstOrDefaultAsync();
                        if (character == null) return false;
                        if (character.AccountId != context.Account.Id) return false;
                        if (character.IsCreated) return false;
                        if (!_characterStatValidationService.IsValid(character.Type, msg.Level, msg.Strength, msg.Stamina, msg.Dexterity, msg.Willpower, msg.StatusPoints)) return false;
                        character.Name = msg.Name;
                        character.Level = msg.Level;
                        character.Strength = msg.Strength;
                        character.Stamina = msg.Stamina;
                        character.Dexterity = msg.Dexterity;
                        character.Willpower = msg.Willpower;
                        character.StatusPoints = msg.StatusPoints;
                        character.IsCreated = true;

                        var home = new Home();
                        home.CharacterId = character.Id;
                        home.Level = 1;
                        uow.Homes.Add(home);

                        try
                        {
                            await uow.CommitAsync();
                            return true;
                        }
                        catch { return false; }
                    }
                }
            }
            return false;
        }
    }
}
