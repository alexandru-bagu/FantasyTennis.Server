using System;

namespace FTServer
{
    public class ItemToolKind
    {
        public static ItemToolKind Fishing { get; } = new ItemToolKind(0, "Fishing");
        public static ItemToolKind Basket { get; } = new ItemToolKind(1, "Basket");

        private int _value;
        private string _text;

        private ItemToolKind(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ItemToolKind Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "fishing") return Fishing;
            if (value == "basket") return Basket;
            throw new Exception($"Unknown value for ItemToolKind: {value}");
        }

        public static implicit operator int(ItemToolKind type) { return type._value; }
        public static implicit operator byte(ItemToolKind type) { return (byte)type._value; }
        public static implicit operator ItemToolKind(byte value) { return (int)value; }
        public static implicit operator ItemToolKind(int value)
        {
            if (Fishing._value == value) return Fishing;
            if (Basket._value == value) return Basket;
            throw new Exception($"Unknown value for ItemToolKind: {value}");
        }
    }
}
