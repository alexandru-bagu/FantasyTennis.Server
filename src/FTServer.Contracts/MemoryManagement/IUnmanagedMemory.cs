using System;

namespace FTServer.Contracts.MemoryManagement
{
    public interface IUnmanagedMemory : IUnmanagedMemoryReader, IUnmanagedMemoryWriter, IDisposable
    {
        IntPtr PointerStart { get; }
        int Size { get; }
        byte[] ToArray();
        void CopyTo(byte[] buffer, int offset);
    }
}
