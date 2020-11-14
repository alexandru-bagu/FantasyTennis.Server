using FTServer.Contracts.Security;
using FTServer.Security;

namespace FTServer.Core.Services.Security
{
    public class CryptographicServiceFactory : ICryptographicServiceFactory
    {
        public ICryptographicService Create(ICryptographicOption decryptOption, ICryptographicOption encryptOption)
        {
            return new CryptographicService((CryptographicOption)decryptOption, (CryptographicOption)encryptOption);
        }

        public ICryptographicService Create()
        {
            return Create(new CryptographicOption(new byte[4], new byte[4]), new CryptographicOption(new byte[4], new byte[4]));
        }
    }
}
