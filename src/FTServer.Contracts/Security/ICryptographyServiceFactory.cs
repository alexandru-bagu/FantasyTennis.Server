namespace FTServer.Contracts.Security
{
    public interface ICryptographyServiceFactory
    {
        ICryptographyService CreatePlain();
        ICryptographyService CreateXor(ICryptographicOption decryptOption, ICryptographicOption encryptOption);
        ICryptographyService CreateBlowfish(ICryptographicOption decryptOption, ICryptographicOption encryptOption);
    }
}
