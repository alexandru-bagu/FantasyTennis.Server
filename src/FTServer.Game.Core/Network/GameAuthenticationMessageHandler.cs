﻿using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Network;
using FTServer.Network.Message.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network
{
    [NetworkMessageHandler(GameAuthenticationRequest.MessageId)]
    public class GameAuthenticationMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly SemaphoreSlim _authenticationSemaphore;

        public GameAuthenticationMessageHandler(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _authenticationSemaphore = new SemaphoreSlim(1);
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Authenticate)) return;
            var success = await Authenticate(message, context);
            if (!success) context.State = GameState.Offline;
            await context.SendAsync(new GameAuthenticationResponse() { Failure = !success });
        }

        private async Task<bool> Authenticate(INetworkMessage message, GameNetworkContext context)
        {
            await _authenticationSemaphore.WaitAsync();
            try
            {
                if (message is GameAuthenticationRequest request)
                {
                    await using (var uow = await _unitOfWorkFactory.Create())
                    {
                        var character = await uow.Characters.Include(p => p.Account)
                            .Where(p => p.Id == request.CharacterId && !p.Account.Online &&
                                p.Account.Id == request.Data.AccountId && p.Account.Key1 == request.Data.Key1 && p.Account.Key2 == request.Data.Key2)
                            .FirstOrDefaultAsync();
                        if (character == null)
                        {
                            return false;
                        }
                        else
                        {
                            context.State = GameState.SynchronizeExperience;
                            context.Character = character;
                            character.Account.Online = true;
                            await uow.CommitAsync();
                            return true;
                        }
                    }
                }
                return false;
            }
            finally
            {
                _authenticationSemaphore.Release();
            }
        }
    }
}
