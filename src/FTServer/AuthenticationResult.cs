namespace FTServer
{
    public enum AuthenticationResult : short
    {
        Unknown = -63,
        InvalidClientVersion = -62,
        CellphoneLocked = -61,
        BlockedAccount = -5,
        InvalidUsername = -4,
        LoginExpired = -3,
        AlreadyLoggedIn = -2,
        InvalidPassword = -1,
        Success = 0,
    }
}
