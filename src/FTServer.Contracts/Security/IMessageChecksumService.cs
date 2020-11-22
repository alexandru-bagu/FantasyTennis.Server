namespace FTServer.Contracts.Security
{
    public interface IMessageChecksumService
    {
        ushort Compute(byte[] buffer, int offset);
        bool Validate(byte[] buffer, int offset, int checksum);
    }
}
