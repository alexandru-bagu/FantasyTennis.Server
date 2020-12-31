using FTServer.Contracts.Game;
using FTServer.Contracts.Network;
using FTServer.Network;
using FTServer.Network.Message.Character;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(ReserveCharacterRequest.MessageId)]
    public class ReserveCharacterMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly ICharacterBuilder _characterBuilder;

        public ReserveCharacterMessageHandler(ICharacterBuilder characterBuilder)
        {
            _characterBuilder = characterBuilder;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (await context.FaultyState(AuthenticationState.Online)) return;
            if (message is ReserveCharacterRequest createCharacter)
            {
                if (!createCharacter.Type.IsStrict(HeroType.Niki) && !createCharacter.Type.IsStrict(HeroType.LunLun))
                {
                    await context.SendAsync(new ReserveCharacterResponse());
                }
                else
                {
                    var character = await _characterBuilder.Create(context.Account.Id, createCharacter.Type);
                    await context.SendAsync(new ReserveCharacterResponse() { CharacterId = character.Id, Type = createCharacter.Type });
                }
            }
        }
    }
}
