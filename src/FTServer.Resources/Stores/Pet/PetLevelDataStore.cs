using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Pet;
using System.Collections.Generic;
using System.Xml;

namespace FTServer.Resources.Stores.Pet
{
    public class PetLevelDataStore : Dictionary<byte, int>, IPetLevelDataStore
    {
        private const string Resource = "Res/Script/PubETC/Ini3/LevelExp_Pet.xml";

        public PetLevelDataStore(IResourceManager resourceManager)
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
