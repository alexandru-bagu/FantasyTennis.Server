using System.Threading.Tasks;

namespace FTServer.Authentication.Core
{
    public interface IAuthenticationSynchronizationContextService
    {
        Task Acquire();
        void Release();
    }
}
