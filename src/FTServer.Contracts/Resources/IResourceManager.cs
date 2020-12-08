namespace FTServer.Contracts.Resources
{
    public interface IResourceManager
    {
        /// <summary>
        /// Converts path from encrypted extension to decrypted extension and vice versa
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string ConvertPath(string path);
        /// <summary>
        /// Returns true if path leads to an encrypted file, false otherwise.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        bool IsResourceEncrypted(string path);
        /// <summary>
        /// Provides resource content
        /// </summary>
        /// <param name="path">Relative to configured client files root. May be absolute paths as well.</param>
        /// <returns></returns>
        IResource GetResource(string path);
        /// <summary>
        /// Provides resource content
        /// </summary>
        /// <param name="path">Relative to configured client files root. May be absolute paths as well.</param>
        /// <returns></returns>
        string ReadResource(string path);
    }
}
