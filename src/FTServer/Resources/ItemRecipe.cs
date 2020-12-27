using FTServer.Resources.Recipe;
using System.Collections.Generic;

namespace FTServer.Resources
{
    public class ItemRecipe
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public ItemUseType UseType { get; set; }
        public int MaxUse { get; set; }
        public int UseCount { get; set; }
        public RecipeKind Kind { get; set; }
        public HeroType Hero { get; set; }
        public bool EnableParcel { get; set; }
        public int RequireGold { get; set; }
        public int MixGaugeTime { get; set; }
        public int HouseLevel { get; set; }
        public int Probability { get; set; }
        public List<RecipeMaterial> Materials { get; set; }
        public List<RecipeMutation> Mutations { get; set; }
    }
}
