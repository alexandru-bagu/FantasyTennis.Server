using FTServer.Contracts.MemoryManagement;
using FTServer.Contracts.Network;
using FTServer.Core.Memory;
using System;

namespace FTServer.Network
{
    public abstract class NetworkMessage : INetworkMessage
    {
        public abstract int MaximumSize { get; }
        public abstract int MessageId { get; }

        public abstract void Deserialize(IUnmanagedMemoryReader reader);
        public abstract void Serialize(IUnmanagedMemoryWriter writer);

        public unsafe void Deserialize(byte[] buffer, int offset)
        {
            fixed (byte* ptr = buffer)
                using (var pointer = new UnmanagedMemory(new IntPtr(ptr + offset), buffer.Length - offset))
                    Deserialize(pointer);
        }

        public unsafe void Serialize(byte[] buffer, int offset)
        {
            fixed (byte* ptr = buffer)
                using (var pointer = new UnmanagedMemory(new IntPtr(ptr + offset), buffer.Length - offset))
                    Serialize(pointer);
        }
    }
}
