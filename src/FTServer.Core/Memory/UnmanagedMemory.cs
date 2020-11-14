using FTServer.Contracts.MemoryManagement;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FTServer.Core.Memory
{
    public unsafe class UnmanagedMemory : IUnmanagedMemory
    {
        private bool _ownsPointer;
        public int Size { get; private set; }
        public IntPtr PointerStart { get; private set; }
        public IntPtr Pointer { get; private set; }

        public int Position { get => (int)(Pointer.ToInt64() - PointerStart.ToInt64()); set => Pointer = new IntPtr(PointerStart.ToInt64() + value); }

        public UnmanagedMemory(int size) : this(Marshal.AllocHGlobal(size), size, true)
        {
        }

        public UnmanagedMemory(IntPtr pointer, int size, bool ownsPointer = false)
        {
            PointerStart = pointer;
            Size = size;
            Pointer = PointerStart;
            _ownsPointer = ownsPointer;
        }


        public void Dispose()
        {
            if (PointerStart != IntPtr.Zero)
            {
                if (_ownsPointer)
                    Marshal.FreeHGlobal(PointerStart);
                PointerStart = IntPtr.Zero;
            }
        }

        public byte[] ToArray()
        {
            byte[] buffer = new byte[Position];
            CopyTo(buffer, 0);
            return buffer;
        }

        public void CopyTo(byte[] buffer, int offset)
        {
            Marshal.Copy(PointerStart, buffer, offset, Position);
        }

        byte IUnmanagedMemoryReader.ReadByte()
        {
            throw new NotImplementedException();
        }

        sbyte IUnmanagedMemoryReader.ReadSByte()
        {
            throw new NotImplementedException();
        }

        ushort IUnmanagedMemoryReader.ReadUInt16()
        {
            throw new NotImplementedException();
        }

        short IUnmanagedMemoryReader.ReadInt16()
        {
            throw new NotImplementedException();
        }

        uint IUnmanagedMemoryReader.ReadUInt32()
        {
            throw new NotImplementedException();
        }

        int IUnmanagedMemoryReader.ReadInt32()
        {
            throw new NotImplementedException();
        }

        ulong IUnmanagedMemoryReader.ReadUInt64()
        {
            throw new NotImplementedException();
        }

        long IUnmanagedMemoryReader.ReadInt64()
        {
            throw new NotImplementedException();
        }

        float IUnmanagedMemoryReader.ReadSingle()
        {
            throw new NotImplementedException();
        }

        double IUnmanagedMemoryReader.ReadDouble()
        {
            throw new NotImplementedException();
        }

        char IUnmanagedMemoryReader.ReadCharacter(Encoding encoding)
        {
            throw new NotImplementedException();
        }

        string IUnmanagedMemoryReader.ReadString(Encoding encoding)
        {
            throw new NotImplementedException();
        }

        byte[] IUnmanagedMemoryReader.ReadBytes(int length)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(byte value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(sbyte value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(ushort value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(short value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(uint value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(int value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(ulong value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(long value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(float value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(double value)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(char value, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(string value, Encoding encoding)
        {
            throw new NotImplementedException();
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(byte[] value)
        {
            throw new NotImplementedException();
        }
    }
}
