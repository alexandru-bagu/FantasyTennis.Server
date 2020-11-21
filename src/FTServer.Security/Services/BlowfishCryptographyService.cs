using FTServer.Contracts.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System;

namespace FTServer.Security.Services
{
    public class BlowfishCryptographyService : ICryptographyService
    {
        private CryptographicOption _encryptOption;
        private CryptographicOption _decryptOption;
        private IBlockCipher _encryptCipher;
        private IBlockCipher _decryptCipher;

        public ICryptographicOption EncryptOption => _encryptOption;
        public ICryptographicOption DecryptOption => _decryptOption;

        public BlowfishCryptographyService(CryptographicOption decryptOption, CryptographicOption encryptOption)
        {
            _encryptOption = encryptOption;
            _decryptOption = decryptOption;

            _encryptCipher = new FantasyTennisBlowfishEngine();
            _decryptCipher = new FantasyTennisBlowfishEngine();

            _encryptCipher.Init(true, new KeyParameter(encryptOption.Key));
            _decryptCipher.Init(false, new KeyParameter(decryptOption.Key));
        }

        public void Decrypt(byte[] buffer, int offset, int length)
        {
            Decrypt(buffer, offset, buffer, offset, length);
        }

        public void Decrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length)
        {
            if (length % 8 != 0) throw new Exception("Length must be a multiple of 8.");
            int offset = 0;
            while (length - offset > 0)
                offset += _decryptCipher.ProcessBlock(srcBuffer, srcOffset + offset, dstBuffer, dstOffset + offset);
        }

        public void Encrypt(byte[] buffer, int offset, int length)
        {
            Decrypt(buffer, offset, buffer, offset, length);
        }

        public void Encrypt(byte[] srcBuffer, int srcOffset, byte[] dstBuffer, int dstOffset, int length)
        {
            if (length % 8 != 0) throw new Exception("Length must be a multiple of 8.");
            int offset = 0;
            while (length - offset > 0)
                offset += _encryptCipher.ProcessBlock(srcBuffer, srcOffset + offset, dstBuffer, dstOffset + offset);
        }

        public void Dispose()
        {
        }
    }
}
