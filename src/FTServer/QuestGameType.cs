using System;

namespace FTServer
{
    public class QuestGameType
    {
        public static QuestGameType Tutorial { get; } = new QuestGameType(0, "Tutorial");
        public static QuestGameType Challenge { get; } = new QuestGameType(1, "Challenge");

        private int _value;
        private string _text;

        private QuestGameType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static QuestGameType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "tutorial") return Tutorial;
            if (value == "challenge") return Challenge;
            throw new Exception($"Unknown value for QuestGameType: {value}");
        }

        public static implicit operator int(QuestGameType shopPriceType) { return shopPriceType._value; }
        public static implicit operator QuestGameType(int value)
        {
            if (Tutorial._value == value) return Tutorial;
            if (Challenge._value == value) return Challenge;
            throw new Exception($"Unknown value for QuestGameType: {value}");
        }
    }
}
