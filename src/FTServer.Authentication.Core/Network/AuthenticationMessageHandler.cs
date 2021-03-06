﻿using FTServer.Contracts.Game;
using FTServer.Contracts.Network;
using FTServer.Contracts.Security;
using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Network;
using FTServer.Network.Message.Authentication;
using FTServer.Network.Message.Character;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(AuthenticationRequest.MessageId)]
    public class AuthenticationMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private const int ClientLongVersion = 21108180;

        private readonly ILogger<AuthenticationMessageHandler> _logger;
        private readonly IAuthenticationSynchronizationContextService _authenticationSynchronizationContextService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ISecureHashProvider _secureHashProvider;
        private readonly ICharacterEquipmentBuilder _characterEquipmentBuilder;

        public AuthenticationMessageHandler(ILogger<AuthenticationMessageHandler> logger, 
            IAuthenticationSynchronizationContextService authenticationSynchronizationContextService,
            IUnitOfWorkFactory unitOfWorkFactory, 
            ISecureHashProvider secureHashProvider,
            ICharacterEquipmentBuilder characterEquipmentBuilder)
        {
            _logger = logger;
            _authenticationSynchronizationContextService = authenticationSynchronizationContextService;
            _unitOfWorkFactory = unitOfWorkFactory;
            _secureHashProvider = secureHashProvider;
            _characterEquipmentBuilder = characterEquipmentBuilder;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (await context.FaultyState(AuthenticationState.Authenticate)) return;
            var result = await Authenticate(message, context);
            if (result == AuthenticationResult.Success)
            {
                await context.SendAsync(new AuthenticationResponse(result)
                {
                    Data = new AccountData()
                    {
                        AccountId = context.Account.Id,
                        Key1 = context.Account.Key1,
                        Key2 = context.Account.Key2,
                    }
                });
                List<Character> characterList = new List<Character>();
                List<Item> itemList = new List<Item>();
                await using (var uow = _unitOfWorkFactory.Create())
                {
                    characterList = await uow.Characters.Where(p => p.AccountId == context.Account.Id).ToListAsync();
                    itemList = await uow.Items.Include(p => p.Character).Where(p => p.Character.AccountId == context.Account.Id && p.Equipped).ToListAsync();
                }

                await context.SendAsync(new CharacterListMessage()
                {
                    Characters = characterList,
                    Items = itemList,
                    EquipmentBuilder = _characterEquipmentBuilder,
                    SelectedCharacterId = context.Account.LastCharacterId
                });
                context.State = AuthenticationState.Online;
            }
            else
            {
                context.State = AuthenticationState.Offline;
                await context.SendAsync(new AuthenticationResponse(result));
            }
        }

        private async Task<AuthenticationResult> Authenticate(INetworkMessage message, AuthenticationNetworkContext context)
        {
            await _authenticationSynchronizationContextService.Acquire();
            try
            {
                AuthenticationResult result = AuthenticationResult.Unknown;
                if (message is AuthenticationRequest request)
                {
                    _logger.LogInformation($"User login attempt: {request.Username} with password: {request.Password} with client version: {request.ClientLongVersion}");
                    try
                    {
                        await using (var uow = _unitOfWorkFactory.Create())
                        {
                            if (request.ClientLongVersion != ClientLongVersion)
                            {
                                result = AuthenticationResult.InvalidClientVersion;
                            }
                            else
                            {
                                var login = await uow.Logins.Where(p => p.Username == request.Username).FirstOrDefaultAsync();
                                if (login == null)
                                {
                                    result = AuthenticationResult.InvalidUsername;
                                }
                                else
                                {
                                    if (login.DisabledUntil != null && login.DisabledUntil > DateTime.Now)
                                    {
                                        result = AuthenticationResult.CellphoneLocked;
                                    }
                                    else
                                    {
                                        var hash = _secureHashProvider.Hash(request.Password + login.Salt);
                                        if (hash != login.Hash)
                                        {
                                            result = AuthenticationResult.InvalidPassword;
                                        }
                                        else
                                        {
                                            var account = await uow.Accounts.Where(p => p.Id == login.AccountId).FirstOrDefaultAsync();
                                            if (account.Online)
                                            {
                                                result = AuthenticationResult.AlreadyLoggedIn;
                                            }
                                            else
                                            {
                                                if (!account.Enabled)
                                                {
                                                    result = AuthenticationResult.BlockedAccount;
                                                }
                                                else
                                                {
                                                    account.Online = true;

                                                    account.Key1 = _secureHashProvider.RandomInt();
                                                    account.Key2 = _secureHashProvider.RandomInt();
                                                    result = AuthenticationResult.Success;
                                                    context.Account = account;
                                                }
                                            }
                                        }
                                    }
                                    uow.LoginAttempts.Add(new LoginAttempt(context.Options.RemoteIPAddress, login.Id, result == AuthenticationResult.Success));
                                    await uow.CommitAsync();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        result = AuthenticationResult.Unknown;
                        _logger.LogError(ex, "Process");
                    }
                }
                return result;
            }
            finally
            {
                _authenticationSynchronizationContextService.Release();
            }
        }
    }
}
