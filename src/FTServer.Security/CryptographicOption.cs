using FTServer.Contracts.Security;

namespace FTServer.Security
{
    public class CryptographicOption : ICryptographicOption
    {
        public byte[] Key { get; }

        public CryptographicOption(byte[] key)
        {
            Key = key;
        }
    }
}
