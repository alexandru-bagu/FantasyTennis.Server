using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores.Item;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Resources.Stores.Item
{
    public class ItemRecipeDataStore : Dictionary<int, ItemRecipe>, IItemRecipeDataStore
    {
        private const string Resource = "Res/Script/PubItem/Ini3/Item_Recipe_Ini3.xml";
        private Dictionary<int, HashSet<int>> _byKind;

        public ItemRecipeDataStore(IResourceManager resourceManager,
            ILogger<ItemRecipeDataStore> logger)
        {
            logger.LogInformation("loading...");
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            foreach (dynamic recipeRes in resource.RecipeList)
            {
                var recipe = new ItemRecipe();
                recipe.Index = recipeRes.Index;
                recipe.Name = recipeRes.Nameen;
                recipe.Name = recipe.Name.Trim();
                recipe.Kind = RecipeKind.Parse(recipeRes.Kind);
                recipe.UseType = ItemUseType.Parse(recipeRes.UseType);
                recipe.MaxUse = recipeRes.MaxUse;
                recipe.UseCount = recipeRes.UseCount;
                recipe.Hero = HeroType.Parse(recipeRes.Character);
                recipe.EnableParcel = recipeRes.EnableParcel == 1;
                recipe.RequireGold = recipeRes.RequireGold;
                recipe.MixGaugeTime = recipeRes.MixGaugeTime;
                recipe.HouseLevel = recipeRes.Houselevel;
                recipe.Probability = recipeRes.Probability;
                recipe.Materials = new List<Recipe.RecipeMaterial>();
                recipe.Mutations = new List<Recipe.RecipeMutation>();
                for (int i = 0; i < 6; i++)
                {
                    if (recipeRes.Members["Material" + i] != 0)
                    {
                        recipe.Materials.Add(new Recipe.RecipeMaterial()
                        {
                            Index = recipeRes.Members["Material" + i],
                            Count = recipeRes.Members["Count" + i],
                        });
                    }
                }
                for (int i = 0; i < 5; i++)
                {
                    if (recipeRes.Members["Mutation" + i] != 0)
                    {
                        recipe.Mutations.Add(new Recipe.RecipeMutation()
                        {
                            Index = recipeRes.Members["Mutation" + i],
                            Probability = recipeRes.Members["MutationPro" + i],
                            Min = recipeRes.Members["MutationMIN" + i],
                            Max = recipeRes.Members["MutationMAX" + i],
                        });
                    }
                }
                Add(recipe.Index, recipe);
            }

            _byKind = new Dictionary<int, HashSet<int>>();
            logger.LogInformation("loaded.");
        }

        public HashSet<int> ByKindAndHero(RecipeKind kind, HeroType hero)
        {
            var key = 1000 + kind * 1000 + hero;
            if (!_byKind.TryGetValue(key, out HashSet<int> set))
                _byKind.Add(key, set = Values.Where(p => p.Kind == kind && p.Hero == hero).Select(p => p.Index).ToHashSet());
            return set;
        }
    }
}
