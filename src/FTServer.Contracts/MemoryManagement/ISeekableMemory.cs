using System;

namespace FTServer.Contracts.MemoryManagement
{
    public interface ISeekableMemory
    {
        IntPtr Pointer { get; }
        int Position { get; set; }
        int Length { get; }
    }
}
