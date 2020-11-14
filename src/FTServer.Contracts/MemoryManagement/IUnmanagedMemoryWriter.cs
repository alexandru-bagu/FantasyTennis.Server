using System;
using System.Text;

namespace FTServer.Contracts.MemoryManagement
{
    public interface IUnmanagedMemoryWriter
    {
        IUnmanagedMemoryWriter Write(byte value);
        IUnmanagedMemoryWriter Write(sbyte value);
        IUnmanagedMemoryWriter Write(ushort value);
        IUnmanagedMemoryWriter Write(short value);
        IUnmanagedMemoryWriter Write(uint value);
        IUnmanagedMemoryWriter Write(int value);
        IUnmanagedMemoryWriter Write(ulong value);
        IUnmanagedMemoryWriter Write(long value);
        IUnmanagedMemoryWriter Write(float value);
        IUnmanagedMemoryWriter Write(double value);
        IUnmanagedMemoryWriter Write(char value, Encoding encoding);
        IUnmanagedMemoryWriter Write(string value, Encoding encoding);
        IUnmanagedMemoryWriter Write(byte[] value);
    }
}
