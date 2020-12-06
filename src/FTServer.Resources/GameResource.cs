using FTServer.Contracts.Resources;
using System;
using System.IO;

namespace FTServer.Resources
{
    public class GameResource : IResource
    {
        private readonly IDisposable[] _dependencies;

        public string Path { get; set; }
        public Stream Stream { get; set; }

        public GameResource(string path, Stream stream, params IDisposable[] dependencies)
        {
            Path = path;
            Stream = stream;
            _dependencies = dependencies;
        }

        public void Dispose()
        {
            if (Stream != null)
            {
                Stream.Dispose();
                foreach (var dependency in _dependencies)
                    dependency.Dispose();
                Stream = null;
            }
        }
    }
}
