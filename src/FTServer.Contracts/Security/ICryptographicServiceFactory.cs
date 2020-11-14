namespace FTServer.Contracts.Security
{
    public interface ICryptographicServiceFactory
    {
        ICryptographicService Create();
        ICryptographicService Create(ICryptographicOption decryptOption, ICryptographicOption encryptOption);
    }
}
