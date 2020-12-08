using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Hero;
using System.Collections.Generic;
using System.Xml;

namespace FTServer.Resources.Stores.Hero
{
    public class HeroLevelDataStore : Dictionary<byte, int>, IHeroLevelDataStore
    {
        private const string Resource = "Res/Script/PubETC/Ini3/LevelExp.xml";

        public HeroLevelDataStore(IResourceManager resourceManager)
        {
            var xml = resourceManager.ReadResource(Resource);

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            var nodes = xmlDocument.GetElementsByTagName("Exp");
            byte level = 0;
            foreach (XmlNode node in nodes)
            {
                var valueAttribute = node.Attributes["Value"];
                this.Add(level, int.Parse(valueAttribute.Value));
                level++;
            }
        }
    }
}
