using FTServer.Contracts.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace FTServer.Security.Services
{
    public class SecureHashProvider : ISecureHashProvider
    {
        private RNGCryptoServiceProvider _random;
        public SecureHashProvider()
        {
            _random = new RNGCryptoServiceProvider();
        }

        public string Hash(string input)
        {
            // Create a SHA256   
            using (SHA256 hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                return HexFromBytes(bytes);
            }
        }
        public string LongHash(string input)
        {
            // Create a SHA512 
            using (SHA512 hash = SHA512.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                return HexFromBytes(bytes);
            }
        }

        public string Random(int length)
        {
            byte[] buffer = new byte[length / 2];
            _random.GetBytes(buffer);
            return HexFromBytes(buffer);
        }

        public int RandomInt()
        {
            byte[] buffer = new byte[4];
            _random.GetBytes(buffer);
            return BitConverter.ToInt32(buffer);
        }

        private string HexFromBytes(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));
            return builder.ToString();
        }
    }

}
