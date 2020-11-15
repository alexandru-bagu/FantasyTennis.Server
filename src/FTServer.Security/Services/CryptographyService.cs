using FTServer.Contracts.Security;

namespace FTServer.Security.Services
{
    public class CryptographyService : ICryptographyService
    {
        private CryptographicOption _encryptOption;
        private CryptographicOption _decryptOption;

        public ICryptographicOption EncryptOption => _encryptOption;
        public ICryptographicOption DecryptOption => _decryptOption;

        public CryptographyService(CryptographicOption decryptOption, CryptographicOption encryptOption)
        {
            _encryptOption = encryptOption;
            _decryptOption = decryptOption;
        }

        public void Decrypt(byte[] buffer, int offset, int length)
        {
            Decrypt(buffer, offset, buffer, offset, length);
        }

        public void Decrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length)
        {
            for (var i = 0; i < length; i++)
                dstBuffer[dstOffset + i] = (byte)(srcBuffer[srcOffset + i] ^ _decryptOption.Key[i % _decryptOption.Key.Length]);
        }

        public void Encrypt(byte[] buffer, int offset, int length)
        {
            Decrypt(buffer, offset, buffer, offset, length);
        }

        public void Encrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length)
        {
            for (var i = 0; i < length; i++)
                dstBuffer[dstOffset + i] = (byte)(srcBuffer[srcOffset + i] ^ _encryptOption.Key[i % _encryptOption.Key.Length]);
        }

        public void Dispose()
        {
        }
    }
}
