using System.Runtime.InteropServices;

namespace FTServer.Contracts.Network
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct NetworkMessageHeader
    {
        [FieldOffset(0)]
        public ushort Serial;

        [FieldOffset(2)]
        public ushort Checksum;

        [FieldOffset(2)]
        public ushort Id;

        [FieldOffset(2)]
        public ushort Length;
    }
}
