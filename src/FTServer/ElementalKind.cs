using System;

namespace FTServer
{
    public class ElementalKind
    {
        public static ElementalKind Jewel { get; } = new ElementalKind(0, "Jewel");
        public static ElementalKind Earth { get; } = new ElementalKind(1, "Earth");
        public static ElementalKind Wind { get; } = new ElementalKind(2, "Wind");
        public static ElementalKind Water { get; } = new ElementalKind(3, "Water");
        public static ElementalKind Fire { get; } = new ElementalKind(4, "Fire");
        public static ElementalKind Strength { get; } = new ElementalKind(5, "Strength");
        public static ElementalKind Stamina { get; } = new ElementalKind(6, "Stamina");
        public static ElementalKind Dexterity { get; } = new ElementalKind(7, "Dexterity");
        public static ElementalKind Willpower { get; } = new ElementalKind(8, "Willpower");

        private int _value;
        private string _text;

        private ElementalKind(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static ElementalKind Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "jewel") return Jewel;
            if (value == "earth") return Earth;
            if (value == "wind") return Wind;
            if (value == "water") return Water;
            if (value == "fire") return Fire;
            if (value == "str") return Strength;
            if (value == "sta") return Stamina;
            if (value == "dex") return Dexterity;
            if (value == "wis") return Willpower;
            throw new Exception($"Unknown value for ElementalKind: {value}");
        }

        public static implicit operator int(ElementalKind type) { return type._value; }
        public static implicit operator byte(ElementalKind type) { return (byte)type._value; }
        public static implicit operator ElementalKind(byte value) { return (int)value; }
        public static implicit operator ElementalKind(int value)
        {
            if (Jewel._value == value) return Jewel;
            if (Earth._value == value) return Earth;
            if (Wind._value == value) return Wind;
            if (Water._value == value) return Water;
            if (Fire._value == value) return Fire;
            if (Strength._value == value) return Strength;
            if (Stamina._value == value) return Stamina;
            if (Dexterity._value == value) return Dexterity;
            if (Willpower._value == value) return Willpower;
            throw new Exception($"Unknown value for ElementalKind: {value}");
        }
    }
}
