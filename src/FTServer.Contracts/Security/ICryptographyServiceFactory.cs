namespace FTServer.Contracts.Security
{
    public interface ICryptographyServiceFactory
    {
        ICryptographyService Create();
        ICryptographyService Create(ICryptographicOption decryptOption, ICryptographicOption encryptOption);
    }
}
