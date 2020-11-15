namespace FTServer.Contracts.Security
{
    public interface IMessageSerialService
    {
        ushort Compute(byte[] buffer, int offset);
    }
}
