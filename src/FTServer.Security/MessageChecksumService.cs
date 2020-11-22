using FTServer.Contracts.Security;

namespace FTServer.Security
{
    public class MessageChecksumService : IMessageChecksumService
    {
        public ushort Compute(byte[] buffer, int offset)
        {
            short s = (short)((short)buffer[offset + 0] + buffer[offset + 1] + buffer[offset + 4] + buffer[offset + 5] + buffer[offset + 6] + buffer[offset + 7]);
            return (ushort)(((uint)s & 0x80000001) == 0 ? s + 1587 : s + 1568);
        }

        public bool Validate(byte[] buffer, int offset, int checksum)
        {
            return Compute(buffer, offset) == checksum;
        }
    }
}
