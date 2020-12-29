using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores.Item
{
    public interface IItemPartDataStore : IReadOnlyDictionary<int, ItemPart>
    {
        HashSet<int> ByTypeAndHero(ShopItemPartType type, HeroType hero);
        HashSet<int> ByHero(HeroType hero);
    }
}
