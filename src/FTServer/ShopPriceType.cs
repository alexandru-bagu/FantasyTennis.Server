﻿using System;

namespace FTServer
{
    public class ShopPriceType
    {
        public static ShopPriceType Ap { get; } = new ShopPriceType(0, "Mint");
        public static ShopPriceType Gold { get; } = new ShopPriceType(1, "Gold");


        private int _value;
        private string _text;

        private ShopPriceType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ShopPriceType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "gold") return Gold;
            if (value == "mint") return Ap;
            throw new Exception($"Unknown value for ShopPriceType: {value}");
        }

        public static implicit operator int(ShopPriceType type) { return type._value; }
        public static implicit operator byte(ShopPriceType type) { return (byte)type._value; }
        public static implicit operator ShopPriceType(byte value) { return (int)value; }
        public static implicit operator ShopPriceType(int value)
        {
            if (Gold._value == value) return Gold;
            if (Ap._value == value) return Ap;
            throw new Exception($"Unknown value for ShopPriceType: {value}");
        }
    }
}
