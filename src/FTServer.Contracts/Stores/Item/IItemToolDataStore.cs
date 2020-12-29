using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores.Item
{
    public interface IItemToolDataStore : IReadOnlyDictionary<int, ItemTool>
    {
    }
}
