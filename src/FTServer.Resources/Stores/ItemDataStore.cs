using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using System.Collections.Generic;
using System.Xml;

namespace FTServer.Resources.Stores.Pet
{
    public class ItemDataStore : Dictionary<int, ItemData>, IItemDataStore
    {
        private const string Resource = "Res/Script/Item/Item_Char.xml";

        public ItemDataStore(IResourceManager resourceManager)
        {
            var xml = resourceManager.ReadResource("Res/Script/Item/Item_Parts.xml");
            var x = Dandraka.XmlUtilities.XmlSlurper.ParseText(xml);
            var list = x.ItemList;
            var item = list[0];
            string xx = item.Foot;
            int yxx = item.Mesh;
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
        }
    }
}
