using System;

namespace FTServer
{
    public class EnchantKind
    {
        public static EnchantKind Jewel { get; } = new EnchantKind(0, "Jewel");
        public static EnchantKind Elemental { get; } = new EnchantKind(1, "Elemental");

        private int _value;
        private string _text;

        private EnchantKind(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static EnchantKind Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "jewel") return Jewel;
            if (value == "elemental") return Elemental;
            throw new Exception($"Unknown value for EnchantKind: {value}");
        }

        public static implicit operator int(EnchantKind type) { return type._value; }
        public static implicit operator byte(EnchantKind type) { return (byte)type._value; }
        public static implicit operator EnchantKind(byte value) { return (int)value; }
        public static implicit operator EnchantKind(int value)
        {
            if (Jewel._value == value) return Jewel;
            if (Elemental._value == value) return Elemental;
            throw new Exception($"Unknown value for EnchantKind: {value}");
        }
    }
}
