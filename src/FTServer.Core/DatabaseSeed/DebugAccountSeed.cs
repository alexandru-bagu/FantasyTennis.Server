using FTServer.Database.Model;
using FTServer.Contracts.Database;
using System.Threading.Tasks;

namespace FTServer.Core.DatabaseSeed
{
    public class DebugAccountSeed : IDataSeeder
    {
        public Task SeedAsync(IDbContext context)
        {
#if DEBUG
            var account = new Account();
            account.Ap = 100000;
            account.SecurityLevel = SecurityLevel.GM;
            context.Accounts.Add(account);
#endif
            return Task.CompletedTask;
        }
    }
}
