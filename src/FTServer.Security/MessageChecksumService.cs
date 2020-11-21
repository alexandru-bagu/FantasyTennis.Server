using FTServer.Contracts.Security;

namespace FTServer.Security
{
    public class MessageChecksumService : IMessageChecksumService
    {
        public ushort Compute(byte[] buffer, int offset)
        {
            var s = (short)(buffer[0] + buffer[1] + buffer[4] + buffer[5] + buffer[6] + buffer[7]);
            return (ushort)((s & 0x80000001) == 0 ? s + 1587 : s + 1568);
        }
    }
}
