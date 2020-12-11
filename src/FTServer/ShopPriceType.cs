using System;

namespace FTServer
{
    public class ShopPriceType
    {
        public static ShopPriceType Gold { get; } = new ShopPriceType(0, "Gold");
        public static ShopPriceType Mint { get; } = new ShopPriceType(1, "Mint");

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
            if (value == "mint") return Mint;
            throw new Exception($"Unknown value for ShopPriceType: {value}");
        }

        public static implicit operator int(ShopPriceType shopPriceType) { return shopPriceType._value; }
        public static implicit operator ShopPriceType(int value)
        {
            if (Gold._value == value) return Gold;
            if (Mint._value == value) return Mint;
            throw new Exception($"Unknown value for ShopPriceType: {value}");
        }
    }
}
