using System.IO;

namespace FTServer.Contracts.Resources
{
    public interface IResourceCryptographyService
    {
        Stream Create(Stream stream);
        Stream Create(Stream stream, string key);
        Stream Create(Stream stream, byte[] key);
    }
}
