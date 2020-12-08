using System.IO;

namespace FTServer.Contracts.Resources
{
    public interface IResourceCryptographyService
    {
        Stream Read(Stream stream);
        Stream Read(Stream stream, string key);
        Stream Read(Stream stream, byte[] key);

        Stream Write(Stream stream);
        Stream Write(Stream stream, string key);
        Stream Write(Stream stream, byte[] key);
    }
}
