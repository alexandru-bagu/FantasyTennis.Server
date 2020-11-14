using System;
using System.Text;

namespace FTServer.Contracts.MemoryManagement
{
    public interface IUnmanagedMemoryReader
    {
        byte ReadByte();
        sbyte ReadSByte();
        ushort ReadUInt16();
        short ReadInt16();
        uint ReadUInt32();
        int ReadInt32();
        ulong ReadUInt64();
        long ReadInt64();
        float ReadSingle();
        double ReadDouble();
        char ReadCharacter(Encoding encoding);
        string ReadString(Encoding encoding);
        byte[] ReadBytes(int length);
    }
}
