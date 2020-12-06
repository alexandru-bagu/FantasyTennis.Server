namespace FTServer.Contracts.Resources
{
    public interface IResourceManager
    {
        IResource GetResource(string path);
    }
}
