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

        public Stream Read(Stream stream)
        {
            return Read(stream, DefaultKey);
        }

        public Stream Read(Stream stream, string key)
        {
            byte[] byteKey = new byte[16];
            Encoding.ASCII.GetBytes(key, 0, key.Length, byteKey, 0);
            return Read(stream, byteKey);
        }

        public Stream Read(Stream stream, byte[] key)
        {
            var crypto = new FantasyTennisRijndaelEngine();
            crypto.Init(false, new KeyParameter(key));
            crypto.ControlByte = (byte)stream.ReadByte();
            return new CryptoStream(stream, crypto, CryptoStreamMode.Read);
        }

        public Stream Write(Stream stream)
        {
            return Write(stream, DefaultKey);
        }

        public Stream Write(Stream stream, string key)
        {
            byte[] byteKey = new byte[16];
            Encoding.ASCII.GetBytes(key, 0, key.Length, byteKey, 0);
            return Write(stream, byteKey);
        }

        public Stream Write(Stream stream, byte[] key)
        {
            var crypto = new FantasyTennisRijndaelEngine();
            crypto.Init(true, new KeyParameter(key));
            stream.Write(new byte[] { 6 });
            return new CryptoStream(stream, crypto, CryptoStreamMode.Write);
        }
    }
}
