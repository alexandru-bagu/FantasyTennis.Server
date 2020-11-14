using System;

namespace FTServer.Contracts.MemoryManagement
{
    public interface IUnmanagedMemory : IUnmanagedMemoryReader, IUnmanagedMemoryWriter, IDisposable
    {
        IntPtr PointerStart { get; }
        IntPtr Pointer { get; }
        int Position { get; set; }
        int Size { get; }
        byte[] ToArray();
        void CopyTo(byte[] buffer, int offset);
    }
}
