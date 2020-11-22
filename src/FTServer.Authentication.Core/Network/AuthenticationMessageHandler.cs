using FTServer.Contracts.Network;
using FTServer.Contracts.Security;
using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Network;
using FTServer.Network.Message.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
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
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly ISecureHashProvider _secureHashProvider;
        private readonly SemaphoreSlim _authenticationSemaphore;

        public AuthenticationMessageHandler(ILogger<AuthenticationMessageHandler> logger, IUnitOfWorkFactory unitOfWorkFactory, ISecureHashProvider secureHashProvider)
        {
            _logger = logger;
            _unitOfWorkFactory = unitOfWorkFactory;
            _secureHashProvider = secureHashProvider;
            _authenticationSemaphore = new SemaphoreSlim(1);
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            await _authenticationSemaphore.WaitAsync();
            try
            {
                if (message is AuthenticationRequest request)
                {
                    _logger.LogInformation($"User login attempt: {request.Username} with password: {request.Password} with client version: {request.ClientLongVersion}");
                    AuthenticationResult result = AuthenticationResult.Unknown;
                    try
                    {
                        await using (var uow = await _unitOfWorkFactory.Create())
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
                                                    result = AuthenticationResult.Success;
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
                    await context.SendAsync(new AuthenticationResponse(result));
                }
            }
            finally
            {
                _authenticationSemaphore.Release();
            }
        }
    }
}
