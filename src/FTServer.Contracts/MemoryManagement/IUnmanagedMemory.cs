using System;

namespace FTServer.Contracts.MemoryManagement
{
    public interface IUnmanagedMemory : IUnmanagedMemoryReader, IUnmanagedMemoryWriter, IDisposable
    {
        IUnmanagedMemoryReader Reader { get; }
        IUnmanagedMemoryWriter Writer { get; }
        IntPtr PointerStart { get; }
        int Size { get; }
        byte[] ToArray();
        void CopyTo(byte[] buffer, int offset);
    }
}
