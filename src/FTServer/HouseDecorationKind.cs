using System;

namespace FTServer
{
    public class HouseDecorationKind
    {
        public static HouseDecorationKind Furniture { get; } = new HouseDecorationKind(0, "Furniture");
        public static HouseDecorationKind Decoration { get; } = new HouseDecorationKind(1, "Decoration");

        private int _value;
        private string _text;

        private HouseDecorationKind(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static HouseDecorationKind Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "deco") return Decoration;
            if (value == "furniture") return Furniture;
            throw new Exception($"Unknown value for HouseDecorationKind: {value}");
        }

        public static implicit operator int(HouseDecorationKind type) { return type._value; }
        public static implicit operator byte(HouseDecorationKind type) { return (byte)type._value; }
        public static implicit operator HouseDecorationKind(byte value) { return (int)value; }
        public static implicit operator HouseDecorationKind(int value)
        {
            if (Decoration._value == value) return Decoration;
            if (Furniture._value == value) return Furniture;
            throw new Exception($"Unknown value for HouseDecorationKind: {value}");
        }
    }
}
