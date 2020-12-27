using System;

namespace FTServer
{
    public class ShopItemPartType
    {
        public static ShopItemPartType Head { get; } = new ShopItemPartType(1, "Head");
        public static ShopItemPartType Upper { get; } = new ShopItemPartType(2, "Upper");
        public static ShopItemPartType Lower { get; } = new ShopItemPartType(3, "Lower");
        public static ShopItemPartType Shoes { get; } = new ShopItemPartType(4, "Shoes");
        public static ShopItemPartType Auxiliary { get; } = new ShopItemPartType(5, "Auxiliary");
        public static ShopItemPartType Racket { get; } = new ShopItemPartType(6, "Racket");

        private int _value;
        private string _text;

        private ShopItemPartType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ShopItemPartType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "head") return Head;
            if (value == "upper") return Upper;
            if (value == "lower") return Lower;
            if (value == "shoes") return Shoes;
            if (value == "auxiliary") return Auxiliary;
            if (value == "racket") return Racket;
            throw new Exception($"Unknown value for ShopItemPartType: {value}");
        }

        public static implicit operator int(ShopItemPartType type) { return type._value; }
        public static implicit operator byte(ShopItemPartType type) { return (byte)type._value; }
        public static implicit operator ShopItemPartType(byte value) { return (int)value; }
        public static implicit operator ShopItemPartType(int value)
        {
            if (Head._value == value) return Head;
            if (Upper._value == value) return Upper;
            if (Lower._value == value) return Lower;
            if (Shoes._value == value) return Shoes;
            if (Auxiliary._value == value) return Auxiliary;
            if (Racket._value == value) return Racket;
            throw new Exception($"Unknown value for ShopItemPartType: {value}");
        }

        public static bool operator ==(ShopItemPartType shopType, ItemPartType itemType)
        {
            if (shopType == Head)
                return itemType == ItemPartType.Dye ||
                    itemType == ItemPartType.Hair ||
                    itemType == ItemPartType.Hat;
            if (shopType == Upper)
                return itemType == ItemPartType.Shirt;
            if (shopType == Lower)
                return itemType == ItemPartType.Pants;
            if (shopType == Shoes)
                return itemType == ItemPartType.Shoes ||
                    itemType == ItemPartType.Socks;
            if (shopType == Auxiliary)
                return itemType == ItemPartType.Bag ||
                    itemType == ItemPartType.Glasses ||
                    itemType == ItemPartType.Gloves;
            if (shopType == Racket)
                return itemType == ItemPartType.Racket;
            return false;
        }

        public static bool operator ==(ItemPartType itemType, ShopItemPartType shopType)
        {
            return shopType == itemType;
        }

        public static bool operator !=(ShopItemPartType shopType, ItemPartType itemType)
        {
            return !(shopType == itemType);
        }

        public static bool operator !=(ItemPartType itemType, ShopItemPartType shopType)
        {
            return !(shopType == itemType);
        }

        public override bool Equals(object obj)
        {
            if (obj is ItemPartType itemType) return itemType == this;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _value;
        }
    }
}
