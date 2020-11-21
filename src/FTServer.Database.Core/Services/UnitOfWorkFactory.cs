using FTServer.Contracts.Database;
using FTServer.Contracts.Services.Database;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace FTServer.Database.Core.Services
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UnitOfWorkFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task<IUnitOfWork> Create()
        {
            var scope = _serviceScopeFactory.CreateScope();
            IUnitOfWork uow = scope.ServiceProvider.Create<UnitOfWork>(scope);
            return Task.FromResult(uow);
        }
    }
}
