namespace FTServer.Game.Core
{
    public interface ICurrentServer
    {
        short Id { get; }
        string Name { get; }
        GameServerType Type { get; }
    }
}
