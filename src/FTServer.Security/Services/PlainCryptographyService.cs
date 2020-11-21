using FTServer.Contracts.Security;
using System;

namespace FTServer.Security
{
    public class PlainCryptographyService : ICryptographyService
    {
        public ICryptographicOption EncryptOption => null;

        public ICryptographicOption DecryptOption => null;

        public void Decrypt(byte[] buffer, int offset, int length)
        {
            Decrypt(buffer, offset, buffer, offset, length);
        }

        public void Decrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length)
        {
            Buffer.BlockCopy(srcBuffer, srcOffset, dstBuffer, dstOffset, length);
        }

        public void Encrypt(byte[] buffer, int offset, int length)
        {
            Encrypt(buffer, offset, buffer, offset, length);
        }

        public void Encrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length)
        {
            Buffer.BlockCopy(srcBuffer, srcOffset, dstBuffer, dstOffset, length);
        }

        public void Dispose() { }
    }
}
