using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FTServer.Resources.Stores
{
    public class TextDataStore : Dictionary<string, string>, ITextDataStore
    {
        private const string ResourceIds = "Res/Script/Text/Text.xml";
        private const string ResourceTexts = "Res/Script/Text/Text_th.xml";
        private const string WindowsLineFeed = "\r\n";
        public TextDataStore(IResourceManager resourceManager, ILogger<TextDataStore> logger)
        {
            logger.LogInformation("loading...");
            var ids = resourceManager.ReadResource(ResourceIds)
                .Split(WindowsLineFeed);
            var texts = resourceManager.ReadResource(ResourceTexts)
                .Split(WindowsLineFeed);
            for (int i = 0; i < ids.Length; i++)
                Add(ids[i], texts[i]);
            logger.LogInformation("loaded.");
        }
    }
}
