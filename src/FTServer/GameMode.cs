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
        public static GameMode Quest { get; } = new GameMode(5, "Quest");
        public static GameMode Emblem { get; } = new GameMode(6, "Emblem");

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
            if (value == "quest") return Quest;
            if (value == "emblem") return Emblem;
            throw new Exception($"Unknown value for GameMode: {value}");
        }

        public static implicit operator int(GameMode type) { return type._value; }
        public static implicit operator GameMode(int value)
        {
            if (Basic._value == value) return Basic;
            if (Battle._value == value) return Battle;
            if (Guardian._value == value) return Guardian;
            if (Fishing._value == value) return Fishing;
            if (TreeShaking._value == value) return TreeShaking;
            if (Quest._value == value) return Quest;
            if (Emblem._value == value) return Emblem;
            throw new Exception($"Unknown value for GameMode: {value}");
        }
    }
}
