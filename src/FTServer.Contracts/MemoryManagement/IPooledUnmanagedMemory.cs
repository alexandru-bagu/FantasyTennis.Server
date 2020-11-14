using System;

namespace FTServer.Contracts.MemoryManagement
{
    public interface IPooledUnmanagedMemory : IDisposable
    {
        IUnmanagedMemory Buffer { get; }
    }
}
