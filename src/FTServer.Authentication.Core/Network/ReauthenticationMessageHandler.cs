using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Network;
using FTServer.Network.Message.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Network
{
    [NetworkMessageHandler(ReauthenticationRequest.MessageId)]
    public class ReauthenticationMessageHandler : INetworkMessageHandler<AuthenticationNetworkContext>
    {
        private readonly IAuthenticationSynchronizationContextService _authenticationSynchronizationContextService;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ReauthenticationMessageHandler(IAuthenticationSynchronizationContextService authenticationSynchronizationContextService, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _authenticationSynchronizationContextService = authenticationSynchronizationContextService;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, AuthenticationNetworkContext context)
        {
            if (await context.FaultyState(AuthenticationState.Authenticate)) return;
            var success = await Authenticate(message, context);
            if (!success) context.State = AuthenticationState.Offline;
            await context.SendAsync(new ReauthenticationResponse() { Failure = !success });
        }

        private async Task<bool> Authenticate(INetworkMessage message, AuthenticationNetworkContext context)
        {
            await _authenticationSynchronizationContextService.Acquire();
            try
            {
                if (message is ReauthenticationRequest request)
                {
                    await using (var uow = await _unitOfWorkFactory.Create())
                    {
                        var account = await uow.Accounts
                            .Where(p => !p.Online && p.Id == request.Data.AccountId && p.Key1 == request.Data.Key1 && p.Key2 == request.Data.Key2)
                            .FirstOrDefaultAsync();
                        
                        if (account == null)
                        {
                            return false;
                        }
                        else
                        {
                            context.Account = account;
                            context.State = AuthenticationState.Online;
                            account.Online = true;
                            await uow.CommitAsync();
                            return true;
                        }
                    }
                }
                return false;
            }
            finally
            {
                _authenticationSynchronizationContextService.Release();
            }
        }
    }
}
