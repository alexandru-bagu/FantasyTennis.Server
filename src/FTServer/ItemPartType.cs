using System;

namespace FTServer
{
    public class ItemPartType
    {
        public static ItemPartType Set { get; } = new ItemPartType(0, "Set");
        public static ItemPartType Hat { get; } = new ItemPartType(1, "Hat");
        public static ItemPartType Shirt { get; } = new ItemPartType(2, "Shirt");
        public static ItemPartType Pants { get; } = new ItemPartType(3, "Pants");
        public static ItemPartType Socks { get; } = new ItemPartType(4, "Socks");
        public static ItemPartType Shoes { get; } = new ItemPartType(5, "Shoes");
        public static ItemPartType Gloves { get; } = new ItemPartType(6, "Glove");
        public static ItemPartType Racket { get; } = new ItemPartType(7, "Racket");
        public static ItemPartType Glasses { get; } = new ItemPartType(7, "Glasses");
        public static ItemPartType Bag { get; } = new ItemPartType(9, "Bag");
        public static ItemPartType Hair { get; } = new ItemPartType(10, "Hair");
        public static ItemPartType Dye { get; } = new ItemPartType(11, "Dye");

        private int _value;
        private string _text;

        private ItemPartType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ItemPartType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "bag") return Bag;
            if (value == "hand") return Gloves;
            if (value == "foot") return Shoes;
            if (value == "pants") return Pants;
            if (value == "cap") return Hat;
            if (value == "racket") return Racket;
            if (value == "body") return Shirt;
            if (value == "glasses") return Glasses;
            if (value == "socks") return Socks;
            if (value == "hair") return Hair;
            if (value == "body") return Shirt;
            if (value == "dye") return Dye;
            throw new Exception($"Unknown value for ItemPartType: {value}");
        }

        public static implicit operator int(ItemPartType type) { return type._value; }
        public static implicit operator byte(ItemPartType type) { return (byte)type._value; }
        public static implicit operator ItemPartType(byte value) { return (int)value; }
        public static implicit operator ItemPartType(int value)
        {
            if (Set._value == value) return Set;
            if (Bag._value == value) return Bag;
            if (Gloves._value == value) return Gloves;
            if (Shoes._value == value) return Shoes;
            if (Pants._value == value) return Pants;
            if (Hat._value == value) return Hat;
            if (Racket._value == value) return Racket;
            if (Shirt._value == value) return Shirt;
            if (Glasses._value == value) return Glasses;
            if (Socks._value == value) return Socks;
            if (Hair._value == value) return Hair;
            if (Shirt._value == value) return Shirt;
            if (Dye._value == value) return Dye;
            throw new Exception($"Unknown value for ItemPartType: {value}");
        }
    }
}
