using FTServer.Database.Model;
using FTServer.Contracts;
using FTServer.Contracts.Database;
using FTServer.Contracts.Services.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FFTServer.Database.Seed.Services
{
    public class DataSeedService : IDataSeedService
    {
        private readonly ILogger<DataSeedService> _logger;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly SemaphoreSlim _semaphoreSlim;
        private bool _ran;

        public DataSeedService(ILogger<DataSeedService> logger, IUnitOfWorkFactory unitOfWorkFactory, IServiceProvider serviceProvider)
        {
            _semaphoreSlim = new SemaphoreSlim(1);
            _ran = false;
            _logger = logger;
            _unitOfWorkFactory = unitOfWorkFactory;
            _serviceProvider = serviceProvider;
        }

        public async Task SeedAsync()
        {
            await _semaphoreSlim.WaitAsync();
            try
            {
                if (_ran) return;
                _ran = true;

                _logger.LogInformation($"Begin {nameof(SeedAsync)}");
                await using (var unitOfWork = _unitOfWorkFactory.Create())
                {
                    var dbContext = unitOfWork.DatabaseContext;
                    var baseType = typeof(IDataSeeder);
                    var seederTypes = new HashSet<Type>();

                    foreach (var asembly in AppDomain.CurrentDomain.GetAssemblies())
                        foreach (var type in asembly.GetTypes())
                            if (baseType.IsAssignableFrom(type) && type != baseType && !type.IsInterface && !type.IsAbstract)
                                seederTypes.Add(type);

                    var queue = new Queue<Type>();
                    var resolvedDependencies = new HashSet<Type>();
                    var processedDependencies = new HashSet<Type>();

                    foreach (var seeder in seederTypes)
                        buildSeedOrder(seederTypes, resolvedDependencies, processedDependencies, queue, seeder);

                    var seedOrder = queue.ToArray();
                    for (var i = 0; i < seedOrder.Length; i++)
                    {
                        var seederType = seedOrder[i];
                        var seeder = (IDataSeeder)ActivatorUtilities.CreateInstance(_serviceProvider, seederType);
                        if (await dbContext.DataSeeds.FirstOrDefaultAsync(p => p.Id == seederType.FullName) == null)
                        {
                            try
                            {
                                _logger.LogDebug($"Starting seeder: {seederType.FullName}");
                                await seeder.SeedAsync(dbContext);
                                dbContext.DataSeeds.Add(new DataSeed() { Id = seederType.FullName });
                                await unitOfWork.CommitAsync();
                                _logger.LogDebug($"Finishing seeder: {seederType.FullName}");
                            }
                            catch (Exception ex)
                            {
                                _logger.LogError($"{nameof(SeedAsync)}", ex);
                                throw new Exception($"Error during seed {seederType.FullName}", ex);
                            }
                        }
                        else
                        {
                            _logger.LogDebug($"Seeder: {seederType.FullName} already ran");
                        }
                    }
                }
                _logger.LogInformation($"End {nameof(SeedAsync)}");
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private static void buildSeedOrder(HashSet<Type> seeders, HashSet<Type> resolvedDependencies, HashSet<Type> processedDependencies, Queue<Type> queue, Type seeder)
        {
            if (!resolvedDependencies.Contains(seeder))
            {
                if (processedDependencies.Add(seeder))
                {
                    var dependencies = seeder.GetCustomAttributes(typeof(DependsOnAttribute), true);

                    List<Type> unmetDependencies = new List<Type>();
                    foreach (DependsOnAttribute dependencyChain in dependencies)
                        foreach (var dependency in dependencyChain.Dependencies)
                            if (!resolvedDependencies.Contains(dependency) && seeders.Contains(dependency))
                                unmetDependencies.Add(dependency);

                    foreach (var dependency in unmetDependencies)
                        buildSeedOrder(seeders, resolvedDependencies, processedDependencies, queue, dependency);

                    resolvedDependencies.Add(seeder);
                    queue.Enqueue(seeder);
                }
                else
                {
                    throw new Exception($"Dependency could not be met for {seeder.FullName}");
                }
            }
        }
    }
}
