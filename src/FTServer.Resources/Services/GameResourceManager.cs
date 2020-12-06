using FTServer.Contracts.Resources;
using FTServer.Resources.Settings;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.IO.Compression;

namespace FTServer.Resources.Services
{
    public class GameResourceManager : IResourceManager
    {
        private readonly string _rootDir;
        private readonly IResourceCryptographyService _resourceCryptographyService;

        public GameResourceManager(IOptions<AppSettings> appSettings, IResourceCryptographyService resourceCryptographyService)
        {
            var settings = appSettings.Value;
            _rootDir = settings.Resources.Path;
            if (_rootDir.StartsWith("~/"))
            {
                _rootDir = _rootDir.Replace("~/", "");
                _rootDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _rootDir);
            }
            _resourceCryptographyService = resourceCryptographyService;
        }

        public IResource GetResource(string path)
        {
            var filePath = Path.Combine(_rootDir, path);
            GameResource res;
            if (File.Exists(filePath))
            {
                var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                res = new GameResource(path, stream);
            }
            else
            {
                res = GetArchivedResource(path);
            }
            if (res == null) throw new FileNotFoundException();
            return res;
        }

        private string GetEncryptedExtension(string path)
        {
            if (path.EndsWith(".xml")) return ".set";
            return null;
        }

        private GameResource GetArchivedResource(string path)
        {
            string original = path;
            var encryptedExtension = GetEncryptedExtension(path);
            do
            {
                path = Path.GetDirectoryName(path);
                if (path.Length != 0)
                {
                    string nonResPath = path;
                    path = path + ".res";
                    string resPath = Path.Combine(_rootDir, path);
                    if (File.Exists(resPath))
                    {
                        var fileStream = new FileStream(resPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Read);
                        var entryName = original.Substring(nonResPath.Length).TrimStart('/');
                        ZipArchiveEntry zipEntry = null;
                        if (encryptedExtension == null)
                        {
                            zipEntry = zipArchive.GetEntry(entryName);
                            return new GameResource(original, zipEntry.Open(), zipArchive, fileStream);
                        }
                        else
                        {
                            zipEntry = zipArchive.GetEntry(Path.ChangeExtension(entryName, encryptedExtension));
                            if (zipEntry == null) return null;
                            var cryptoStream = _resourceCryptographyService.Create(zipEntry.Open());
                            return new GameResource(original, cryptoStream, zipArchive, fileStream);
                        }
                    }
                }
            } while (!string.IsNullOrWhiteSpace(path));
            return null;
        }
    }
}
