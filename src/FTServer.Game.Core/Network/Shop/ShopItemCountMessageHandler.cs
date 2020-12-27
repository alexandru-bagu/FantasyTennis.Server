using FTServer.Contracts.Network;
using FTServer.Contracts.Stores;
using FTServer.Network;
using FTServer.Network.Message.Shop;
using FTServer.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Shop
{
    [NetworkMessageHandler(ShopOpenRequest.MessageId)]
    public class ShopItemCountMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IShopItemDataStore _shopItemDataStore;
        private readonly IItemPartDataStore _itemPartDataStore;

        public ShopItemCountMessageHandler(IShopItemDataStore shopItemDataStore, IItemPartDataStore itemPartDataStore)
        {
            _shopItemDataStore = shopItemDataStore;
            _itemPartDataStore = itemPartDataStore;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is ShopItemCountRequest shopRequest)
            {
                IEnumerable<ShopItem> list = null;
                if (shopRequest.Category == ItemCategoryType.Parts)
                {
                    list = _shopItemDataStore.Values.Where(p => p.CategoryType == shopRequest.Category && p.Hero == shopRequest.Hero);
                    list = list.Where(p => p.Enable);
                    if (shopRequest.Part == ItemPartType.Set) list = list.Where(p => p.Item0 > 0 && p.Item1 > 0);
                    else
                    {
                        var hs = _itemPartDataStore.Values.Where(p => p.Hero == shopRequest.Hero && p.Type == shopRequest.Part).Select(p => p.Index).ToHashSet();
                        list = list.Where(p => hs.Contains(p.Index));
                    }
                }

                await context.SendAsync(new ShopItemCountResponse()
                {
                    Category = shopRequest.Category,
                    Part = shopRequest.Part,
                    Hero = shopRequest.Hero,
                    Count = list.Count()
                });
            }
        }
    }
}
