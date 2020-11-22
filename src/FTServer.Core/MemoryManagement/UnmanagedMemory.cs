using FTServer.Contracts.MemoryManagement;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace FTServer.Core.MemoryManagement
{
    public unsafe class UnmanagedMemory : IUnmanagedMemory
    {
        private bool _ownsPointer;
        private byte* _startPointer;
        private byte* _currentPointer;
        public int Size { get; private set; }
        public IntPtr PointerStart { get => new IntPtr(_startPointer); }
        public IntPtr Pointer { get => new IntPtr(_currentPointer); }
        public int Position { get => (int)(_currentPointer - _startPointer); set => _currentPointer = _startPointer + value; }
        public int Length { get => Size - Position; }
        public IUnmanagedMemoryReader Reader => this;
        public IUnmanagedMemoryWriter Writer => this;
        public UnmanagedMemory(int size) : this(Marshal.AllocHGlobal(size), size, true)
        {
        }

        public UnmanagedMemory(IntPtr pointer, int size, bool ownsPointer = false)
        {
            _startPointer = (byte*)pointer.ToPointer();
            _currentPointer = _startPointer;
            _ownsPointer = ownsPointer;
            Size = size;
        }


        public void Dispose()
        {
            if (PointerStart != IntPtr.Zero)
            {
                if (_ownsPointer)
                    Marshal.FreeHGlobal(PointerStart);
                _startPointer = (byte*)IntPtr.Zero.ToPointer();
                _currentPointer = (byte*)IntPtr.Zero.ToPointer();
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

        #region IUnmanagedMemoryReader
        byte IUnmanagedMemoryReader.ReadByte()
        {
            try { return *_currentPointer; }
            finally { _currentPointer++; }
        }

        sbyte IUnmanagedMemoryReader.ReadSByte()
        {
            try { return (sbyte)*_currentPointer; }
            finally { _currentPointer++; }
        }

        ushort IUnmanagedMemoryReader.ReadUInt16()
        {
            try { return *(ushort*)_currentPointer; }
            finally { _currentPointer += 2; }
        }

        short IUnmanagedMemoryReader.ReadInt16()
        {
            try { return *(short*)_currentPointer; }
            finally { _currentPointer += 2; }
        }

        uint IUnmanagedMemoryReader.ReadUInt32()
        {
            try { return *(uint*)_currentPointer; }
            finally { _currentPointer += 4; }
        }

        int IUnmanagedMemoryReader.ReadInt32()
        {
            try { return *(int*)_currentPointer; }
            finally { _currentPointer += 4; }
        }

        ulong IUnmanagedMemoryReader.ReadUInt64()
        {
            try { return *(ulong*)_currentPointer; }
            finally { _currentPointer += 8; }
        }

        long IUnmanagedMemoryReader.ReadInt64()
        {
            try { return *(long*)_currentPointer; }
            finally { _currentPointer += 8; }
        }

        float IUnmanagedMemoryReader.ReadSingle()
        {
            try { return *(float*)_currentPointer; }
            finally { _currentPointer += 4; }
        }

        double IUnmanagedMemoryReader.ReadDouble()
        {
            try { return *(double*)_currentPointer; }
            finally { _currentPointer += 8; }
        }

        private static int _asciiCharSize = Encoding.ASCII.GetBytes("\0").Length;
        private static int _unicodeCharSize = Encoding.Unicode.GetBytes("\0").Length;
        private static int _getCharSize(Encoding encoding)
        {
            if (encoding != Encoding.ASCII && encoding != Encoding.Unicode)
                throw new Exception($"Only ASCII and Unicode are supported.");
            var charSize = _asciiCharSize;
            if (encoding == Encoding.Unicode) charSize = _unicodeCharSize;
            return charSize;
        }

        char IUnmanagedMemoryReader.ReadCharacter(Encoding encoding)
        {
            var charSize = _getCharSize(encoding);
            try
            {
                char[] c = new char[1];
                fixed (char* cptr = c) encoding.GetChars(_currentPointer, charSize, cptr, 1);
                return c[0];
            }
            finally { _currentPointer += charSize; }
        }

        string IUnmanagedMemoryReader.ReadString(Encoding encoding)
        {
            var charSize = _getCharSize(encoding);
            IUnmanagedMemoryReader reader = this;
            var currentPtr = _currentPointer;
            while (!reader.ReadBytes(charSize).IsZero()) ;
            var length = _currentPointer - currentPtr - charSize;
            return encoding.GetString(currentPtr, (int)length);
        }

        byte[] IUnmanagedMemoryReader.ReadBytes(int length)
        {
            try
            {
                byte[] bytes = new byte[length];
                Marshal.Copy(new IntPtr(_currentPointer), bytes, 0, length);
                return bytes;
            }
            finally { _currentPointer += length; }
        }
        #endregion
        #region IUnmanagedMemoryWriter
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(byte value)
        {
            try { *_currentPointer = value; return this; }
            finally { _currentPointer++; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(sbyte value)
        {
            try { *(sbyte*)_currentPointer = value; return this; }
            finally { _currentPointer++; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(ushort value)
        {
            try { *(ushort*)_currentPointer = value; return this; }
            finally { _currentPointer += 2; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(short value)
        {
            try { *(short*)_currentPointer = value; return this; }
            finally { _currentPointer += 2; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(uint value)
        {
            try { *(uint*)_currentPointer = value; return this; }
            finally { _currentPointer += 4; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(int value)
        {
            try { *(int*)_currentPointer = value; return this; }
            finally { _currentPointer += 4; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(ulong value)
        {
            try { *(ulong*)_currentPointer = value; return this; }
            finally { _currentPointer += 8; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(long value)
        {
            try { *(long*)_currentPointer = value; return this; }
            finally { _currentPointer += 8; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(float value)
        {
            try { *(float*)_currentPointer = value; return this; }
            finally { _currentPointer += 4; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(double value)
        {
            try { *(double*)_currentPointer = value; return this; }
            finally { _currentPointer += 8; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(char value, Encoding encoding)
        {
            int size = 0;
            try
            {
                var chars = new char[1] { value };
                fixed (char* cptr = chars)
                {
                    size = encoding.GetBytes(cptr, 1, _currentPointer, Size - Position);
                    return this;
                }
            }
            finally { _currentPointer += size; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(string value, Encoding encoding)
        {
            int size = 0;
            try
            {
                var @null = value + "\0";
                fixed (char* cptr = @null)
                {
                    size = encoding.GetBytes(cptr, @null.Length, _currentPointer, Size - Position);
                    return this;
                }
            }
            finally { _currentPointer += size; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.Write(byte[] value)
        {
            try
            {
                Marshal.Copy(value, 0, new IntPtr(_currentPointer), value.Length);
                return this;
            }
            finally { _currentPointer += value.Length; }
        }

        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteByte(byte value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteSByte(sbyte value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteUInt16(ushort value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteInt16(short value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteUInt32(uint value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteInt32(int value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteUInt64(ulong value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteInt64(long value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteSingle(float value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteDouble(double value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteCharacter(char value, Encoding encoding) { IUnmanagedMemoryWriter writer = this; writer.Write(value, encoding); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteString(string value, Encoding encoding) { IUnmanagedMemoryWriter writer = this; writer.Write(value, encoding); return this; }
        IUnmanagedMemoryWriter IUnmanagedMemoryWriter.WriteBytes(byte[] value) { IUnmanagedMemoryWriter writer = this; writer.Write(value); return this; }
        #endregion
    }
}
