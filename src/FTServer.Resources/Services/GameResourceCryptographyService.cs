using FTServer.Contracts.Resources;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FTServer.Resources.Services
{
    public class GameResourceCryptographyService : IResourceCryptographyService
    {
        private const string DefaultKey = "TIMOTEI_ZION";

        public Stream Create(Stream stream)
        {
            return Create(stream, DefaultKey);
        }

        public Stream Create(Stream stream, string key)
        {
            byte[] byteKey = new byte[16];
            Encoding.ASCII.GetBytes(key, 0, key.Length, byteKey, 0);
            return Create(stream, byteKey);
        }

        public Stream Create(Stream stream, byte[] key)
        {
            stream.Read(new byte[1]);
            var crypto = new FantasyTennisRijndaelEngine();
            crypto.Init(false, new KeyParameter(key));
            return new CryptoStream(stream, crypto, CryptoStreamMode.Read);
        }
    }
}
