using FTServer.Contracts.MemoryManagement;
using FTServer.Contracts.Network;
using FTServer.Core.MemoryManagement;
using System;

namespace FTServer.Network
{
    public abstract class NetworkMessage : INetworkMessage
    {
        public abstract int MaximumSize { get; }
        public INetworkMessageHeader Header { get; set; }

        public NetworkMessage(ushort messageId)
        {
            Header = new NetworkMessageHeader() { MessageId = messageId };
        }

        public unsafe virtual void Deserialize(IUnmanagedMemoryReader reader)
        {
            Header = *(NetworkMessageHeader*)reader.Pointer.ToPointer();
            reader.Position += 8;
        }

        public unsafe virtual void Serialize(IUnmanagedMemoryWriter writer)
        {
            Header.Serialize(writer);
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
                    ((NetworkMessageHeader*)ptr)->BodyLength = (ushort)(pointer.Position - 8);
                    return pointer.Position;
                }
            }
        }
    }
}
