using System;
using System.Text;

namespace FTServer.Contracts.MemoryManagement
{
    public interface IUnmanagedMemoryWriter : ISeekableMemory
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

        IUnmanagedMemoryWriter WriteByte(byte value);
        IUnmanagedMemoryWriter WriteSByte(sbyte value);
        IUnmanagedMemoryWriter WriteUInt16(ushort value);
        IUnmanagedMemoryWriter WriteInt16(short value);
        IUnmanagedMemoryWriter WriteUInt32(uint value);
        IUnmanagedMemoryWriter WriteInt32(int value);
        IUnmanagedMemoryWriter WriteUInt64(ulong value);
        IUnmanagedMemoryWriter WriteInt64(long value);
        IUnmanagedMemoryWriter WriteSingle(float value);
        IUnmanagedMemoryWriter WriteDouble(double value);
        IUnmanagedMemoryWriter WriteCharacter(char value, Encoding encoding);
        IUnmanagedMemoryWriter WriteString(string value, Encoding encoding);
        IUnmanagedMemoryWriter WriteBytes(byte[] value);
    }
}
