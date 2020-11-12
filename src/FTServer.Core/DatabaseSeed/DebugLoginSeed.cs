using FTServer.Database.Model;
using FTServer.Contracts;
using FTServer.Contracts.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FTServer.Contracts.Security;

namespace FTServer.Core.DatabaseSeed
{
    [DependsOn(typeof(DebugAccountSeed))]
    public class DebugLoginSeed : IDataSeeder
    {
        private readonly ISecureHashProvider _secureHashProvider;

        public DebugLoginSeed(ISecureHashProvider secureHashProvider)
        {
            _secureHashProvider = secureHashProvider;
        }

        public async Task SeedAsync(IDbContext context)
        {
#if DEBUG
            var account = await context.Accounts.FirstAsync();
            var login = new Login();
            login.AccountId = account.Id;
            login.Username = "test";
            login.Salt = _secureHashProvider.Random(64);
            login.Hash = _secureHashProvider.Hash("test" + login.Salt);
            login.Email = "test@test.test";
            context.Logins.Add(login);
#endif
        }
    }
}

