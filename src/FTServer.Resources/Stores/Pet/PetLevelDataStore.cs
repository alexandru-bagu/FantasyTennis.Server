using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Pet;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FTServer.Resources.Stores.Pet
{
    public class PetLevelDataStore : Dictionary<byte, int>, IPetLevelDataStore
    {
        private const string Resource = "Res/Script/PubETC/Ini3/LevelExp_Pet.xml";

        public PetLevelDataStore(IResourceManager resourceManager,
            ILogger<PetLevelDataStore> logger)
        {
            logger.LogInformation("loading...");
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            byte level = 0;
            foreach (dynamic exp in resource.ExpList)
                Add(level++, exp.Value);
            logger.LogInformation("loaded.");
        }
    }
}
