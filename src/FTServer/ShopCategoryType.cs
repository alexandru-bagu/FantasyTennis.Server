using System;

namespace FTServer
{
    public class ShopCategoryType
    {
        public static ShopCategoryType Char { get; } = new ShopCategoryType(0, "Char");
        public static ShopCategoryType Parts { get; } = new ShopCategoryType(1, "Parts");
        public static ShopCategoryType Special { get; } = new ShopCategoryType(2, "Special");
        public static ShopCategoryType House { get; } = new ShopCategoryType(3, "House");
        public static ShopCategoryType HouseDecoration { get; } = new ShopCategoryType(4, "House Decoration");
        public static ShopCategoryType Recipe { get; } = new ShopCategoryType(5, "Recipe");
        public static ShopCategoryType Quick { get; } = new ShopCategoryType(6, "Quick");
        public static ShopCategoryType Material { get; } = new ShopCategoryType(7, "Material");
        public static ShopCategoryType PetItem { get; } = new ShopCategoryType(8, "Pet Item");
        public static ShopCategoryType PetChar { get; } = new ShopCategoryType(9, "Pet Char");
        public static ShopCategoryType Card { get; } = new ShopCategoryType(10, "Card");
        public static ShopCategoryType Guild { get; } = new ShopCategoryType(11, "Guild");
        public static ShopCategoryType Enchant { get; } = new ShopCategoryType(12, "Enchant");
        public static ShopCategoryType Tool { get; } = new ShopCategoryType(13, "Tool");
        public static ShopCategoryType Lottery { get; } = new ShopCategoryType(14, "Lottery");
        public static ShopCategoryType Ball { get; } = new ShopCategoryType(15, "Ball");

        private int _value;
        private string _text;

        private ShopCategoryType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ShopCategoryType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "char") return Char;
            if (value == "parts") return Parts;
            if (value == "special") return Special;
            if (value == "house") return House;
            if (value == "house_deco") return HouseDecoration;
            if (value == "recipe") return Recipe;
            if (value == "quick") return Quick;
            if (value == "material") return Material;
            if (value == "pet_item") return PetItem;
            if (value == "pet_char") return PetChar;
            if (value == "card") return Card;
            if (value == "guild") return Guild;
            if (value == "enchant") return Enchant;
            if (value == "tool") return Tool;
            if (value == "lottery") return Lottery;
            if (value == "ball") return Ball;
            throw new Exception($"Unknown value for ShopCategoryType: {value}");
        }

        public static implicit operator int(ShopCategoryType type) { return type._value; }
        public static implicit operator ShopCategoryType(int value)
        {
            if (Char._value == value) return Char;
            if (Parts._value == value) return Parts;
            if (Special._value == value) return Special;
            if (House._value == value) return House;
            if (HouseDecoration._value == value) return HouseDecoration;
            if (Recipe._value == value) return Recipe;
            if (Quick._value == value) return Quick;
            if (Material._value == value) return Material;
            if (PetItem._value == value) return PetItem;
            if (PetChar._value == value) return PetChar;
            if (Card._value == value) return Card;
            if (Guild._value == value) return Guild;
            if (Enchant._value == value) return Enchant;
            if (Tool._value == value) return Tool;
            if (Lottery._value == value) return Lottery;
            if (Ball._value == value) return Ball;
            throw new Exception($"Unknown value for ShopCategoryType: {value}");
        }
    }
}
