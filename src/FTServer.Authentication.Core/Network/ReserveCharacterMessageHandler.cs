﻿using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Network;
using FTServer.Network.Message.Character;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(ReserveCharacterRequest.MessageId)]
    public class ReserveCharacterMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly ILogger<DefaultNetworkMessageHandler> _logger;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ReserveCharacterMessageHandler(ILogger<DefaultNetworkMessageHandler> logger, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _logger = logger;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (message is ReserveCharacterRequest createCharacter)
            {
                if (createCharacter.Type != CharacterType.Niki && createCharacter.Type != CharacterType.LunLun)
                {
                    await context.SendAsync(new ReserveCharacterResponse());
                }
                else
                {
                    var character = new Character();
                    await using (var uow = await _unitOfWorkFactory.Create())
                    {
                        character.Type = createCharacter.Type;
                        character.IsCreated = false;
                        character.NameChangeAllowed = false;
                        character.AccountId = context.Account.Id;
                        character.Level = 1;
                        uow.Characters.Add(character);
                        await uow.CommitAsync();
                    }
                    await context.SendAsync(new ReserveCharacterResponse() { CharacterId = character.Id, Type = createCharacter.Type });
                }
            }
        }
    }
}