namespace FTServer.Contracts.Security
{
    public interface IMessageChecksumService
    {
        ushort Compute(byte[] buffer, int offset);
    }
}
