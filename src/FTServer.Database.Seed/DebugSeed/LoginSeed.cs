using FTServer.Database.Model;
using FTServer.Contracts;
using FTServer.Contracts.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FTServer.Contracts.Security;

namespace FTServer.Database.Seed.DebugSeed
{
    [DependsOn(typeof(AccountSeed))]
    public class LoginSeed : IDataSeeder
    {
        private readonly ISecureHashProvider _secureHashProvider;

        public LoginSeed(ISecureHashProvider secureHashProvider)
        {
            _secureHashProvider = secureHashProvider;
        }

        public async Task SeedAsync(IUnitOfWork uow)
        {
#if DEBUG
            var account = await uow.Accounts.FirstAsync();
            var login = new Login();
            login.AccountId = account.Id;
            login.Username = "test";
            login.Salt = _secureHashProvider.Random(64);
            login.Hash = _secureHashProvider.Hash("test" + login.Salt);
            login.Email = "test@test.test";
            uow.Logins.Add(login);
#endif
        }
    }
}

