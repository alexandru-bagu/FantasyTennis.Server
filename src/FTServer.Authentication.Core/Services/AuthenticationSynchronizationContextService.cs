using System.Threading;
using System.Threading.Tasks;

namespace FTServer.Authentication.Core.Services
{
    public class AuthenticationSynchronizationContextService : IAuthenticationSynchronizationContextService
    {
        private readonly SemaphoreSlim _authenticationSemaphore;

        public AuthenticationSynchronizationContextService()
        {
            _authenticationSemaphore = new SemaphoreSlim(1);
        }

        public Task Acquire()
        {
            return _authenticationSemaphore.WaitAsync();
        }

        public void Release()
        {
            _authenticationSemaphore.Release();
        }
    }
}
