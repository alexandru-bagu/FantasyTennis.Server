using FTServer.Contracts.Security;

namespace FTServer.Security
{
    public class CryptographicOption : ICryptographicOption
    {
        public byte[] Key { get; }
        public byte[] IV { get; }

        public CryptographicOption(byte[] key, byte[] iv)
        {
            Key = key;
            IV = iv;
        }
    }
}
