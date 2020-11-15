using FTServer.Contracts.Security;

namespace FTServer.Security
{
    public class NullCryptographyService : ICryptographyService
    {
        public ICryptographicOption EncryptOption => null;

        public ICryptographicOption DecryptOption => null;

        public void Decrypt(byte[] buffer, int offset, int length) { }

        public void Decrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length) { }

        public void Encrypt(byte[] buffer, int offset, int length) { }

        public void Encrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length) { }

        public void Dispose() { }
    }
}
