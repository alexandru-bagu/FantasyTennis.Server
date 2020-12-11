using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Hero;
using System.Collections.Generic;

namespace FTServer.Resources.Stores.Hero
{
    public class HeroLevelDataStore : Dictionary<byte, int>, IHeroLevelDataStore
    {
        private const string Resource = "Res/Script/PubETC/Ini3/LevelExp.xml";

        public HeroLevelDataStore(IResourceManager resourceManager)
        {
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            byte level = 0;
            foreach (dynamic exp in resource.ExpList)
                Add(level++, exp.Value);
        }
    }
}
