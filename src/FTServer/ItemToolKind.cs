using System;

namespace FTServer
{
    public class ItemUseType
    {
        public static ItemUseType NotAvailable { get; } = new ItemUseType(0, "N/A");
        public static ItemUseType Time { get; } = new ItemUseType(1, "Time");
        public static ItemUseType Count { get; } = new ItemUseType(2, "Count");
        public static ItemUseType Durable { get; } = new ItemUseType(3, "Durable");
        public static ItemUseType Instant { get; } = new ItemUseType(4, "Instant");

        private int _value;
        private string _text;

        private ItemUseType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ItemUseType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "n/a") return NotAvailable;
            if (value == "durable") return Durable;
            if (value == "time") return Time;
            if (value == "instant") return Instant;
            if (value == "count") return Count;
            throw new Exception($"Unknown value for ItemUseType: {value}");
        }

        public static implicit operator int(ItemUseType type) { return type._value; }
        public static implicit operator byte(ItemUseType type) { return (byte)type._value; }
        public static implicit operator ItemUseType(byte value) { return (int)value; }
        public static implicit operator ItemUseType(int value)
        {
            if (NotAvailable._value == value) return NotAvailable;
            if (Durable._value == value) return Durable;
            if (Time._value == value) return Time;
            if (Instant._value == value) return Instant;
            if (Count._value == value) return Count;
            throw new Exception($"Unknown value for ItemUseType: {value}");
        }
    }
}
