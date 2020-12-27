using System;

namespace FTServer
{
    public class RecipeKind
    {
        public static RecipeKind CharItem { get; } = new RecipeKind(0, "CharItem");
        public static RecipeKind QuickSlot { get; } = new RecipeKind(1, "QuickSlot");
        public static RecipeKind PetItem { get; } = new RecipeKind(2, "PetItem");
        public static RecipeKind EnchantItem { get; } = new RecipeKind(3, "EnchantItem");
        public static RecipeKind EtcItem { get; } = new RecipeKind(4, "EtcItem");

        private int _value;
        private string _text;

        private RecipeKind(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static RecipeKind Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "etc_item") return EtcItem;
            if (value == "quick_slot") return QuickSlot;
            if (value == "pet_item") return PetItem;
            if (value == "char_item") return CharItem;
            if (value == "enchant_item") return EnchantItem;
            throw new Exception($"Unknown value for RecipeKind: {value}");
        }

        public static implicit operator int(RecipeKind type) { return type._value; }
        public static implicit operator byte(RecipeKind type) { return (byte)type._value; }
        public static implicit operator RecipeKind(byte value) { return (int)value; }
        public static implicit operator RecipeKind(int value)
        {
            if (EtcItem._value == value) return EtcItem;
            if (QuickSlot._value == value) return QuickSlot;
            if (PetItem._value == value) return PetItem;
            if (CharItem._value == value) return CharItem;
            if (EnchantItem._value == value) return EnchantItem;
            throw new Exception($"Unknown value for RecipeKind: {value}");
        }
    }
}
