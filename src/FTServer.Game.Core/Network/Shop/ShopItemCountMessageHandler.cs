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
    [NetworkMessageHandler(ShopItemCountRequest.MessageId)]
    public class ShopItemCountMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IShopItemDataStore _shopItemDataStore;

        public ShopItemCountMessageHandler(IShopItemDataStore shopItemDataStore)
        {
            _shopItemDataStore = shopItemDataStore;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is ShopItemCountRequest shopRequest)
            {
                IEnumerable<ShopItem> list = _shopItemDataStore.Search(shopRequest.Category, shopRequest.Part, shopRequest.Hero);

                await context.SendAsync(new ShopItemCountResponse()
                {
                    Category = shopRequest.Category,
                    Part = shopRequest.Part,
                    Hero = shopRequest.Hero,
                    Pages = list.Count() 
                });
            }
        }
    }
}
