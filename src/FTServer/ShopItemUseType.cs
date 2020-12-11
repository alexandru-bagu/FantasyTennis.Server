using System;

namespace FTServer
{
    public class ShopItemUseType
    {
        public static ShopItemUseType NotAvailable { get; } = new ShopItemUseType(-1, "N/A");
        public static ShopItemUseType Durable { get; } = new ShopItemUseType(0, "Durable");
        public static ShopItemUseType Time { get; } = new ShopItemUseType(1, "Time");
        public static ShopItemUseType Instant { get; } = new ShopItemUseType(2, "Instant");
        public static ShopItemUseType Count { get; } = new ShopItemUseType(3, "Count");

        private int _value;
        private string _text;

        private ShopItemUseType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ShopItemUseType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "n/a") return NotAvailable;
            if (value == "durable") return Durable;
            if (value == "time") return Time;
            if (value == "instant") return Instant;
            if (value == "count") return Count;
            throw new Exception($"Unknown value for ShopItemUseType: {value}");
        }

        public static implicit operator int(ShopItemUseType shopPriceType) { return shopPriceType._value; }
        public static implicit operator ShopItemUseType(int value)
        {
            if (NotAvailable._value == value) return NotAvailable;
            if (Durable._value == value) return Durable;
            if (Time._value == value) return Time;
            if (Instant._value == value) return Instant;
            if (Count._value == value) return Count;
            throw new Exception($"Unknown value for ShopItemUseType: {value}");
        }
    }
}
