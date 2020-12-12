using FTServer.Contracts.Services.Database;
using FTServer.Database.Model;
using FTServer.Game.Core.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Services
{
    public class ConcurrentUserTrackingService : IConcurrentUserTrackingService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly AppSettings _options;
        private SemaphoreSlim _semaphore;
        private GameServer _gameServer;

        public ConcurrentUserTrackingService(IUnitOfWorkFactory unitOfWorkFactory, IOptions<AppSettings> options)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _options = options.Value;
            _semaphore = new SemaphoreSlim(1);
        }

        public async Task Reset()
        {
            await _semaphore.WaitAsync();
            try
            {
                await using (var uow = await _unitOfWorkFactory.Create())
                {
                    _gameServer = await uow.GameServers.Where(p => p.Name == _options.GameServer.Name).FirstOrDefaultAsync();
                    _gameServer.OnlineCount = 0;
                    await uow.CommitAsync();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task Increment()
        {
            await _semaphore.WaitAsync();
            try
            {
                await using (var uow = await _unitOfWorkFactory.Create())
                {
                    uow.Attach(_gameServer);
                    _gameServer.OnlineCount += 1;
                    await uow.CommitAsync();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task Decrement()
        {
            await _semaphore.WaitAsync();
            try
            {
                await using (var uow = await _unitOfWorkFactory.Create())
                {
                    uow.Attach(_gameServer);
                    _gameServer.OnlineCount -= 1;
                    await uow.CommitAsync();
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
