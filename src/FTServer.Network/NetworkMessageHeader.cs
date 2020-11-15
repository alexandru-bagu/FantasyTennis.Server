using FTServer.Contracts.MemoryManagement;
using FTServer.Contracts.Network;
using FTServer.Core.MemoryManagement;
using System;
using System.Runtime.InteropServices;

namespace FTServer.Network
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct NetworkMessageHeader : INetworkMessageHeader
    {
        [FieldOffset(0)]
        public ushort Serial;

        [FieldOffset(2)]
        public ushort Checksum;

        [FieldOffset(4)]
        public ushort MessageId;

        [FieldOffset(6)]
        public ushort BodyLength;

        ushort INetworkMessageHeader.Serial { get => Serial; set => Serial = value; }
        ushort INetworkMessageHeader.Checksum { get => Checksum; set => Checksum = value; }
        ushort INetworkMessageHeader.MessageId { get => MessageId; set => MessageId = value; }
        ushort INetworkMessageHeader.BodyLength { get => BodyLength; set => BodyLength = value; }

        public unsafe void Deserialize(IUnmanagedMemoryReader reader)
        {
            throw new NotImplementedException("Raw manipulation is faster.");
        }

        public unsafe void Serialize(IUnmanagedMemoryWriter writer)
        {
            *((NetworkMessageHeader*)writer.Pointer.ToPointer()) = this;
            writer.Position += 8;
        }

        public unsafe void Deserialize(byte[] buffer, int offset)
        {
            fixed (byte* ptr = buffer)
                using (var pointer = new UnmanagedMemory(new IntPtr(ptr + offset), buffer.Length - offset))
                    Deserialize(pointer);
        }

        public unsafe int Serialize(byte[] buffer, int offset)
        {
            fixed (byte* ptr = buffer)
            {
                using (var pointer = new UnmanagedMemory(new IntPtr(ptr + offset), buffer.Length - offset))
                {
                    Serialize(pointer);
                    return pointer.Position;
                }
            }
        }
    }
}
