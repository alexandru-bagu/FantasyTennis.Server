using System;

namespace FTServer
{
    public class HeroType
    {
        public static HeroType All { get; } = new HeroType(-1, "All");
        public static HeroType Niki { get; } = new HeroType(0, "Niki");
        public static HeroType LunLun { get; } = new HeroType(1, "LunLun");
        public static HeroType Dennis { get; } = new HeroType(2, "Dennis");
        public static HeroType Lucy { get; } = new HeroType(3, "Lucy");
        public static HeroType Shua { get; } = new HeroType(4, "Shua");
        public static HeroType Pochi { get; } = new HeroType(5, "Pochi");
        public static HeroType Al { get; } = new HeroType(6, "Al");

        private int _value;
        private string _text;

        private HeroType(int value, string text)
        {
            _value = value;
            _text = text;
        }
        public override string ToString()
        {
            return _text;
        }

        public static HeroType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "niki") return Niki;
            if (value == "lunlun") return LunLun;
            if (value == "dennis" || value == "dhanpir") return Dennis;
            if (value == "lucy") return Lucy;
            if (value == "shua") return Shua;
            if (value == "pochi") return Pochi;
            if (value == "al") return Al;
            if (value == "all") return All;
            throw new Exception($"Unknown value for HeroType: {value}");
        }

        public static implicit operator int(HeroType type) { return type._value; }
        public static implicit operator byte(HeroType type) { return (byte)type._value; }
        public static implicit operator HeroType(int value)
        {
            if (Niki._value == value) return Niki;
            if (LunLun._value == value) return LunLun;
            if (Dennis._value == value) return Dennis;
            if (Lucy._value == value) return Lucy;
            if (Shua._value == value) return Shua;
            if (Pochi._value == value) return Pochi;
            if (Al._value == value) return Al;
            if (All._value == value) return All;
            throw new Exception($"Unknown value for HeroType: {value}");
        }
    }
}
