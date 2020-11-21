using FTServer.Contracts.Database;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FTServer.Database.Core
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ILogger<UnitOfWork> _logger;
        private readonly IRawDbContext _databaseContext;
        private readonly IServiceScope _serviceScope;

        public IDbContext DatabaseContext { get => _databaseContext; }
        public bool AutoCommit { get; set; }

        private IDbContextTransaction _transaction;

        public UnitOfWork(ILogger<UnitOfWork> logger, IRawDbContext databaseContext, IServiceScope serviceScope
            /* we keep `IServiceScope` referenced to prevent GC from removing it before we dispose of it*/)
        {
            _logger = logger;
            _databaseContext = databaseContext;
            _serviceScope = serviceScope;
            _transaction = _databaseContext.Database.BeginTransaction();
        }

        public async Task CommitAsync()
        {
            _logger.LogTrace("Begin CommitAsync");
            try
            {
                await _databaseContext.SaveChangesAsync();
                await _transaction.CommitAsync();
                _transaction = await _databaseContext.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CommitAsync");
                throw new Exception("CommitAsync", ex);
            }
            finally
            {
                _logger.LogTrace("End CommitAsync");
            }
        }

        public async Task AbortAsync()
        {
            _logger.LogTrace("Begin AbortAsync");
            try
            {
                await _transaction.RollbackAsync();
                await _databaseContext.Database.RollbackTransactionAsync();
                _transaction = await _databaseContext.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AbortAsync");
                throw new Exception("AbortAsync", ex);
            }
            finally
            {
                _logger.LogTrace("End AbortAsync");
            }
        }

        public void Dispose()
        {
            _logger.LogTrace("Begin Dispose");
            try
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _databaseContext.Dispose();
                _serviceScope.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dispose");
                throw new Exception("Dispose", ex);
            }
            finally
            {
                _logger.LogTrace("End AbortAsync");
            }
        }

        public async ValueTask DisposeAsync()
        {
            _logger.LogTrace("Begin DisposeAsync");
            try
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                await _databaseContext.DisposeAsync();
                _serviceScope.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DisposeAsync");
                throw new Exception("DisposeAsync", ex);
            }
            finally
            {
                _logger.LogTrace("End DisposeAsync");
            }
        }
    }
}