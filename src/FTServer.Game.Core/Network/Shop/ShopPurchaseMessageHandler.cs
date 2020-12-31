using FTServer.Contracts.Game;
using FTServer.Contracts.Network;
using FTServer.Contracts.Services.Database;
using FTServer.Contracts.Stores;
using FTServer.Contracts.Stores.Item;
using FTServer.Database.Model;
using FTServer.Network;
using FTServer.Network.Message.Character;
using FTServer.Network.Message.Shop;
using FTServer.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTServer.Game.Core.Network.Shop
{
    [NetworkMessageHandler(ShopPurchaseRequest.MessageId)]
    public class ShopPurchaseMessageHandler : INetworkMessageHandler<GameNetworkContext>
    {
        public const int MaxCharacters = 7;

        private readonly IShopItemDataStore _shopItemDataStore;
        private readonly IItemPartDataStore _itemPartDataStore;
        private readonly ICharacterBuilder _characterBuilder;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ShopPurchaseMessageHandler(IShopItemDataStore shopItemDataStore, IItemPartDataStore itemPartDataStore, ICharacterBuilder characterBuilder, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _shopItemDataStore = shopItemDataStore;
            _itemPartDataStore = itemPartDataStore;
            _characterBuilder = characterBuilder;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task Process(INetworkMessage message, GameNetworkContext context)
        {
            if (await context.FaultyState(GameState.Online)) return;
            if (message is ShopPurchaseRequest shopRequest)
            {
                int goldCost = 0, apCost = 0, charmCost = 0, newCharacters = 0;
                var result = ValidatePurchase(context, shopRequest, ref goldCost, ref apCost, ref charmCost, ref newCharacters);
                if (result == ShopPurchaseResponse.ShopPurchaseResult.Success)
                {
                    if (!context.HasGold(goldCost))
                    {
                        result = ShopPurchaseResponse.ShopPurchaseResult.NeedMoreGold;
                    }
                    else if (!context.HasAp(apCost))
                    {
                        result = ShopPurchaseResponse.ShopPurchaseResult.NeedMoreAp;
                    }
                    else if (!context.HasCharm(charmCost))
                    {
                        result = ShopPurchaseResponse.ShopPurchaseResult.NotEnoughCharmPoints;
                    }
                    if (newCharacters > 0)
                    {
                        await using (var uow = _unitOfWorkFactory.Create())
                        {
                            var currentCount = await uow.Characters.Where(p => p.AccountId == context.Character.AccountId).CountAsync();

                            if (newCharacters + currentCount > MaxCharacters)
                                result = ShopPurchaseResponse.ShopPurchaseResult.CharacterLimit;
                        }
                    }
                }

                var newItems = new List<Item>();
                if (result == ShopPurchaseResponse.ShopPurchaseResult.Success)
                {
                    if (!await context.AddCurrency(-goldCost, -apCost, -charmCost))
                    {
                        result = ShopPurchaseResponse.ShopPurchaseResult.CashInfoFailed;
                    }
                    else
                    {
                        await using (var uow = _unitOfWorkFactory.Create())
                        {
                            foreach (var purchaseItem in shopRequest.Items)
                            {
                                if (_shopItemDataStore.TryGetValue(purchaseItem.Index, out ShopItem shopItem))
                                {
                                    if (shopItem.GoldBack > 0)
                                        await context.AddGold(shopItem.GoldBack);
                                    if (shopItem.Item1 != 0) // set
                                    {
                                        int characterId = context.Character.Id;
                                        if (!shopItem.Hero.IsStrict(HeroType.All))
                                        {
                                            var character = await _characterBuilder.Create(context.Character.AccountId, shopItem.Hero);
                                            characterId = character.Id;
                                        }

                                        foreach (var index in shopItem.Items)
                                        {
                                            var item = new Item()
                                            {
                                                CharacterId = characterId,
                                                CategoryType = ShopCategoryType.Parts,
                                                Index = index,
                                                UseType = shopItem.UseType,
                                                Quantity = 1,
                                            };
                                            uow.Items.Add(item);
                                            if (characterId == context.Character.Id)
                                                newItems.Add(item);
                                        }
                                    }
                                    else
                                    {
                                        var item = context.Items.FirstOrDefault(p => p.Index == shopItem.Item0);
                                        if (item == null)
                                        {
                                            item = new Item()
                                            {
                                                CharacterId = context.Character.Id,
                                                CategoryType = shopItem.CategoryType,
                                                Index = shopItem.Item0,
                                                UseType = shopItem.UseType
                                            };
                                            uow.Items.Add(item);
                                            context.Items.Add(item);
                                        }
                                        else
                                        {
                                            uow.Attach(item);
                                        }
                                        if (purchaseItem.Option == 0)
                                            item.Quantity += shopItem.Use0 == 0 ? 1 : shopItem.Use0;
                                        else if (purchaseItem.Option == 1)
                                            item.Quantity += shopItem.Use1;
                                        else if (purchaseItem.Option == 2)
                                            item.Quantity += shopItem.Use2;
                                        if (shopItem.UseType == ItemUseType.Time)
                                            item.ExpirationDate = DateTime.UtcNow.AddDays(item.Quantity);
                                    }
                                }
                            }
                            await uow.CommitAsync();
                        }
                    }
                }

                await context.SendAsync(new ShopPurchaseResponse()
                {
                    Result = result,
                    Items = newItems
                });

                if (result == ShopPurchaseResponse.ShopPurchaseResult.Success)
                {
                    await context.SendAsync(new SynchronizeCurrencyResponse()
                    {
                        Ap = context.Ap,
                        Gold = context.Gold
                    });
                }
            }
        }

        private ShopPurchaseResponse.ShopPurchaseResult ValidatePurchase(GameNetworkContext context, ShopPurchaseRequest shopRequest, ref int goldCost, ref int apCost, ref int charmCost, ref int newCharacters)
        {
            int count = 0;
            foreach (var item in shopRequest.Items)
            {
                if (_shopItemDataStore.TryGetValue(item.Index, out ShopItem shopItem))
                {
                    if (shopItem.Enable == false && shopItem.CategoryType != ShopCategoryType.Parts)
                        return ShopPurchaseResponse.ShopPurchaseResult.NotForSale;

                    if (shopItem.CategoryType == ShopCategoryType.Parts)
                        if (_itemPartDataStore.TryGetValue(shopItem.Item0, out ItemPart itemPart))
                            if (!itemPart.Hero.Is(context.Character.HeroType) && shopItem.Hero.IsStrict(HeroType.All))
                                return ShopPurchaseResponse.ShopPurchaseResult.NotForSaleForCurrentHero;

                    if (shopItem.PriceType == ShopPriceType.Gold)
                        goldCost += shopItem.Price0;
                    else if (shopItem.PriceType == ShopPriceType.Ap)
                        apCost += shopItem.Price0;
                    if (shopItem.Couple)
                        charmCost += shopItem.CouplePrice;

                    if (shopItem.Item1 != 0 && !shopItem.Hero.IsStrict(HeroType.All))
                        newCharacters += 1;
                    count += shopItem.Items.Length;
                }
            }
            if (context.Items.Count + count > context.Character.PocketSize)
                return ShopPurchaseResponse.ShopPurchaseResult.PocketSizeLimit;
            return ShopPurchaseResponse.ShopPurchaseResult.Success;
        }
    }
}
