namespace FTServer.Contracts.Security
{
    public interface ISecureHashProvider
    {
        string Hash(string input);
        string LongHash(string input);
        string Random(int length);
        int RandomInt();
    }
}
