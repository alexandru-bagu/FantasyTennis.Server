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
            _rootDir = settings.Resources?.Path ?? "";
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
            if (res == null) throw new FileNotFoundException($"File {path} not found.");
            return res;
        }

        public string ConvertPath(string path)
        {
            var ext = GetConversionExtension(path);
            return Path.ChangeExtension(path, ext);
        }

        public bool IsResourceEncrypted(string path)
        {
            var ext = Path.GetExtension(path);
            return ext == ".set";
        }

        private string GetConversionExtension(string path)
        {
            if (path.EndsWith(".xml")) return ".set";
            else if (path.EndsWith(".set")) return ".xml";
            return Path.GetExtension(path) + ".unk";
        }

        private GameResource GetArchivedResource(string path)
        {
            string original = path;
            var encryptedExtension = GetConversionExtension(path);
            do
            {
                path = Path.GetDirectoryName(path);
                if (path.Length != 0)
                {
                    string nonResPath = path;
                    path = path + ".res";
                    string resPath = path;
                    if (!string.IsNullOrWhiteSpace(_rootDir))
                        resPath = Path.Combine(_rootDir, path);
                    if (File.Exists(resPath))
                    {
                        var fileStream = new FileStream(resPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Read);
                        var entryName = original.Substring(nonResPath.Length).TrimStart('/');
                        ZipArchiveEntry zipEntry;
                        if (encryptedExtension == null)
                        {
                            zipEntry = zipArchive.GetEntry(entryName);
                            return new GameResource(original, zipEntry.Open(), zipArchive, fileStream);
                        }
                        else
                        {
                            zipEntry = zipArchive.GetEntry(Path.ChangeExtension(entryName, encryptedExtension));
                            if (zipEntry == null) return null;
                            var stream = zipEntry.Open();
                            var cryptoStream = _resourceCryptographyService.Read(stream);
                            return new GameResource(original, cryptoStream, zipArchive, fileStream);
                        }
                    }
                }
            } while (!string.IsNullOrWhiteSpace(path));
            return null;
        }

        public string ReadResource(string path)
        {
            using (var resource = GetResource(path))
            using (var reader = new StreamReader(resource.Stream))
                return reader.ReadToEnd().Trim('\0');
        }
    }
}
