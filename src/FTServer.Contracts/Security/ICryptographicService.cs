using System;

namespace FTServer.Contracts.Security
{
    public interface ICryptographicService : IDisposable
    {
        ICryptographicOption EncryptOption { get; }
        void Encrypt(byte[] buffer, int offset, int length);
        void Encrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length);
        ICryptographicOption DecryptOption { get; }
        void Decrypt(byte[] buffer, int offset, int length);
        void Decrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length);
    }
}
