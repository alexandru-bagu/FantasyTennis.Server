﻿using FTServer.Resources;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores.Item
{
    public interface IItemRecipeDataStore : IReadOnlyDictionary<int, ItemRecipe>
    {
        HashSet<int> ByKindAndHero(RecipeKind kind, HeroType hero);
    }
}
