using System;

namespace FTServer
{
    public class ItemCategoryType
    {
        public static ItemCategoryType Char { get; } = new ItemCategoryType(0, "Char");
        public static ItemCategoryType Parts { get; } = new ItemCategoryType(1, "Parts");
        public static ItemCategoryType Special { get; } = new ItemCategoryType(2, "Special");
        public static ItemCategoryType Quick { get; } = new ItemCategoryType(3, "Quick");
        public static ItemCategoryType Card { get; } = new ItemCategoryType(4, "Card");
        public static ItemCategoryType Tool { get; } = new ItemCategoryType(5, "Tool");
        public static ItemCategoryType PetChar { get; } = new ItemCategoryType(6, "Pet Char");
        public static ItemCategoryType HouseDecoration { get; } = new ItemCategoryType(7, "House Decoration");
        public static ItemCategoryType Material { get; } = new ItemCategoryType(8, "Material");
        public static ItemCategoryType Recipe { get; } = new ItemCategoryType(9, "Recipe");
        public static ItemCategoryType PetItem { get; } = new ItemCategoryType(10, "Pet Item");
        public static ItemCategoryType House { get; } = new ItemCategoryType(11, "House");
        public static ItemCategoryType Skill { get; } = new ItemCategoryType(12, "Skill");
        public static ItemCategoryType Lottery { get; } = new ItemCategoryType(13, "Lottery");
        public static ItemCategoryType Guild { get; } = new ItemCategoryType(14, "Guild");
        public static ItemCategoryType Enchant { get; } = new ItemCategoryType(15, "Enchant");
        public static ItemCategoryType Ball { get; } = new ItemCategoryType(16, "Ball");

        private int _value;
        private string _text;

        private ItemCategoryType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ItemCategoryType Parse(string value)
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
            if (value == "skill") return Skill;
            if (value == "tool") return Tool;
            if (value == "lottery") return Lottery;
            if (value == "ball") return Ball;
            throw new Exception($"Unknown value for ItemCategoryType: {value}");
        }

        public static implicit operator int(ItemCategoryType type) { return type._value; }
        public static implicit operator byte(ItemCategoryType type) { return (byte)type._value; }
        public static implicit operator ItemCategoryType(byte value) { return (int)value; }
        public static implicit operator ItemCategoryType(int value)
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
            if (Skill._value == value) return Skill;
            if (Tool._value == value) return Tool;
            if (Lottery._value == value) return Lottery;
            if (Ball._value == value) return Ball;
            throw new Exception($"Unknown value for ItemCategoryType: {value}");
        }
    }
}
