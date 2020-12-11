using System;

namespace FTServer
{
    public class GameMode
    {
        public static GameMode Basic { get; } = new GameMode(0, "Basic");
        public static GameMode Battle { get; } = new GameMode(1, "Battle");
        public static GameMode Guardian { get; } = new GameMode(2, "Guardian");
        public static GameMode Fishing { get; } = new GameMode(3, "Fishing");
        public static GameMode TreeShaking { get; } = new GameMode(4, "TreeShaking");

        private int _value;
        private string _text;

        private GameMode(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static GameMode Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "basic") return Basic;
            if (value == "battle") return Battle;
            if (value == "guardian") return Guardian;
            if (value == "fishing") return Fishing;
            if (value == "apple") return TreeShaking;
            throw new Exception($"Unknown value for GameMode: {value}");
        }

        public static implicit operator int(GameMode shopPriceType) { return shopPriceType._value; }
        public static implicit operator GameMode(int value)
        {
            if (Basic._value == value) return Basic;
            if (Battle._value == value) return Battle;
            if (Guardian._value == value) return Guardian;
            if (Fishing._value == value) return Fishing;
            if (TreeShaking._value == value) return TreeShaking;
            throw new Exception($"Unknown value for GameMode: {value}");
        }
    }
}
