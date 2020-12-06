using System;
using System.IO;

namespace FTServer.Contracts.Resources
{
    public interface IResource : IDisposable
    {
        public string Path { get; }
        public Stream Stream { get; }
    }
}
