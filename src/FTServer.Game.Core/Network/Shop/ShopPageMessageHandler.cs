using FTServer.Contracts.Network;
using FTServer.Contracts.Stores;
using FTServer.Network;
using FTServer.Network.Message.Shop;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Shop
{
    [NetworkMessageHandler(ShopPageRequest.MessageId)]
    public class ShopPageMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        private readonly IShopItemDataStore _shopItemDataStore;

        public ShopPageMessageHandler(IShopItemDataStore shopItemDataStore)
        {
            _shopItemDataStore = shopItemDataStore;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is ShopPageRequest shopRequest)
            {
                const int pageSize = 6;
                await context.SendAsync(new ShopPageResponse()
                {
                    Items = _shopItemDataStore.Search(shopRequest.Category, shopRequest.Part, shopRequest.Hero)
                        .Skip((shopRequest.Page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList()
                });
            }
        }
    }
}
