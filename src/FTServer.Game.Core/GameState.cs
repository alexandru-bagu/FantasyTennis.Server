namespace FTServer.Game.Core
{
    public enum GameState
    {
        Offline = -1,
        Authenticate = 0,
        SynchronizeExperience,
        SynchronizeHome,
        SynchronizeInventory,
        SynchronizeUnknown1,
        SynchronizeCoupleSystem,
        Online
    }
}
