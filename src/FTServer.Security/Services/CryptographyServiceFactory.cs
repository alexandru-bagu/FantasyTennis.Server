using FTServer.Contracts.Security;

namespace FTServer.Security.Services
{
    public class CryptographyServiceFactory : ICryptographyServiceFactory
    {
        public ICryptographyService CreatePlain()
        {
            return new PlainCryptographyService();
        }

        public ICryptographyService CreateXor(ICryptographicOption decryptOption, ICryptographicOption encryptOption)
        {
            return new XorCryptographyService((CryptographicOption)decryptOption, (CryptographicOption)encryptOption);
        }

        public ICryptographyService CreateBlowfish(ICryptographicOption decryptOption, ICryptographicOption encryptOption)
        {
            return new BlowfishCryptographyService((CryptographicOption)decryptOption, (CryptographicOption)encryptOption);
        }
    }
}
