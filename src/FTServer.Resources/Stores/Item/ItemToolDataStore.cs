using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Item;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FTServer.Resources.Stores.Item
{
    public class ItemToolDataStore : Dictionary<int, ItemTool>, IItemToolDataStore
    {
        private const string Resource = "Res/Script/Item/Item_Tools.xml";

        public ItemToolDataStore(IResourceManager resourceManager,
            ILogger<ItemToolDataStore> logger)
        {
            logger.LogInformation("loading...");
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            foreach (dynamic toolRes in resource.RecipeList)
            {
                var tool = new ItemTool();
                tool.Index = toolRes.Index;
                tool.Name = toolRes.Nameen;
                tool.Name = tool.Name.Trim();
                tool.Kind = ItemToolKind.Parse(toolRes.Kind);
                tool.UseType = ItemUseType.Parse(toolRes.UseType);
                tool.MaxUse = toolRes.MaxUse;
                tool.EnableParcel = toolRes.EnableParcel == 1;

                Add(tool.Index, tool);
            }
            logger.LogInformation("loading...");
        }
    }
}
