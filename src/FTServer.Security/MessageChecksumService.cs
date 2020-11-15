using FTServer.Contracts.Security;

namespace FTServer.Security
{
    public class MessageChecksumService : IMessageChecksumService
    {
        public ushort Compute(byte[] buffer, int offset)
        {
            var s = buffer[0] + buffer[1] + buffer[4] + buffer[5] + buffer[6] + buffer[7];
            return (ushort)(s + (1567 + ((s & 1) ^ 1)));
        }
    }
}
