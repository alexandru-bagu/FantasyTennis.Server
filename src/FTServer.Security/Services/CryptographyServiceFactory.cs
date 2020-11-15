using FTServer.Contracts.Security;

namespace FTServer.Security.Services
{
    public class CryptographyServiceFactory : ICryptographyServiceFactory
    {
        public ICryptographyService Create(ICryptographicOption decryptOption, ICryptographicOption encryptOption)
        {
            return new CryptographyService((CryptographicOption)decryptOption, (CryptographicOption)encryptOption);
        }

        public ICryptographyService Create()
        {
            return Create(new CryptographicOption(new byte[4], new byte[4]), new CryptographicOption(new byte[4], new byte[4]));
        }
    }
}
