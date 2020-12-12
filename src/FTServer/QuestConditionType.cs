using System;

namespace FTServer
{
    public class QuestConditionType
    {
        public static QuestConditionType Tutorial { get; } = new QuestConditionType(0, "Tutorial");
        public static QuestConditionType BattleMonLevel { get; } = new QuestConditionType(1, "BattleMonLevel");
        public static QuestConditionType Fishes { get; } = new QuestConditionType(2, "Fishes");
        public static QuestConditionType Fruits { get; } = new QuestConditionType(3, "Fruits");
        public static QuestConditionType Furniture { get; } = new QuestConditionType(4, "Furniture");
        public static QuestConditionType Transmutes { get; } = new QuestConditionType(5, "Transmutes");
        public static QuestConditionType Smash { get; } = new QuestConditionType(6, "Smash");
        public static QuestConditionType Slice { get; } = new QuestConditionType(7, "Slice");
        public static QuestConditionType ChargeShot { get; } = new QuestConditionType(8, "ChargeShot");
        public static QuestConditionType Lob { get; } = new QuestConditionType(9, "Lob");
        public static QuestConditionType SkillShot { get; } = new QuestConditionType(10, "SkillShot");
        public static QuestConditionType CharacterLevel { get; } = new QuestConditionType(11, "CharacterLevel");
        public static QuestConditionType WinCount { get; } = new QuestConditionType(12, "WinCount");
        public static QuestConditionType LoseCount { get; } = new QuestConditionType(13, "LoseCount");
        public static QuestConditionType ReturnAce { get; } = new QuestConditionType(14, "ReturnAce");
        public static QuestConditionType ServiceAce { get; } = new QuestConditionType(15, "ServiceAce");
        public static QuestConditionType GuardBreak { get; } = new QuestConditionType(16, "GuardBreak");
        public static QuestConditionType PerfectGame { get; } = new QuestConditionType(17, "PerfectGame");
        public static QuestConditionType WorldQuest { get; } = new QuestConditionType(18, "WorldQuest");
        public static QuestConditionType TotalSmash { get; } = new QuestConditionType(19, "TotalSmash");
        public static QuestConditionType TotalSlice { get; } = new QuestConditionType(20, "TotalSlice");
        public static QuestConditionType TotalChargeShot { get; } = new QuestConditionType(21, "TotalChargeShot");
        public static QuestConditionType TotalLob { get; } = new QuestConditionType(22, "TotalLob");
        public static QuestConditionType TotalServiceAce { get; } = new QuestConditionType(23, "TotalServiceAce");
        public static QuestConditionType TotalReturnAce { get; } = new QuestConditionType(24, "TotalReturnAce");
        public static QuestConditionType TotalSkillShot { get; } = new QuestConditionType(25, "TotalSkillShot");
        public static QuestConditionType TotalWinCount { get; } = new QuestConditionType(26, "TotalWinCount");
        public static QuestConditionType TotalLoseCount { get; } = new QuestConditionType(27, "TotalLoseCount");
        public static QuestConditionType TotalGuardBreak { get; } = new QuestConditionType(28, "TotalGuardBreak");
        public static QuestConditionType TotalPerfectGame { get; } = new QuestConditionType(29, "TotalPerfectGame");
        public static QuestConditionType TotalFishes { get; } = new QuestConditionType(30, "TotalFishes");
        public static QuestConditionType TotalFruits { get; } = new QuestConditionType(31, "TotalFruits");

        private int _value;
        private string _text;

        private QuestConditionType(int value, string text)
        {
            _value = value;
            _text = text;
        }

        public override string ToString()
        {
            return _text;
        }

        public static QuestConditionType Parse(string value)
        {
            value = value.ToLowerInvariant();
            if (value == "tutorial") return Tutorial;
            if (value == "battlemonlevel") return BattleMonLevel;
            if (value == "fishes") return Fishes;
            if (value == "fruits") return Fruits;
            if (value == "furniture") return Furniture;
            if (value == "transmutes") return Transmutes;
            if (value == "smash") return Smash;
            if (value == "slice") return Slice;
            if (value == "chargeshot") return ChargeShot;
            if (value == "lob") return Lob;
            if (value == "skillshot") return SkillShot;
            if (value == "characterlevel") return CharacterLevel;
            if (value == "wincount") return WinCount;
            if (value == "losecount") return LoseCount;
            if (value == "returnace") return ReturnAce;
            if (value == "serviceace") return ServiceAce;
            if (value == "guardbreak") return GuardBreak;
            if (value == "perfectgame") return PerfectGame;
            if (value == "worldquest") return WorldQuest;
            if (value == "totalsmash") return TotalSmash;
            if (value == "totalslice") return TotalSlice;
            if (value == "totalchargeshot") return TotalChargeShot;
            if (value == "totallob") return TotalLob;
            if (value == "totalserviceace") return TotalServiceAce;
            if (value == "totalreturnace") return TotalReturnAce;
            if (value == "totalskillshot") return TotalSkillShot;
            if (value == "totalwincount") return TotalWinCount;
            if (value == "totallosecount") return TotalLoseCount;
            if (value == "totalguardbreak") return TotalGuardBreak;
            if (value == "totalperfectgame") return TotalPerfectGame;
            if (value == "totalfishes") return TotalFishes;
            if (value == "totalfruits") return TotalFruits;
            throw new Exception($"Unknown value for QuestConditionType: {value}");
        }

        public static implicit operator int(QuestConditionType type) { return type._value; }
        public static implicit operator QuestConditionType(int value)
        {
            if (Tutorial._value == value) return Tutorial;
            if (BattleMonLevel._value == value) return BattleMonLevel;
            if (Fishes._value == value) return Fishes;
            if (Fruits._value == value) return Fruits;
            if (Furniture._value == value) return Furniture;
            if (Transmutes._value == value) return Transmutes;
            if (Smash._value == value) return Smash;
            if (Slice._value == value) return Slice;
            if (ChargeShot._value == value) return ChargeShot;
            if (Lob._value == value) return Lob;
            if (SkillShot._value == value) return SkillShot;
            if (CharacterLevel._value == value) return CharacterLevel;
            if (WinCount._value == value) return WinCount;
            if (LoseCount._value == value) return LoseCount;
            if (ReturnAce._value == value) return ReturnAce;
            if (ServiceAce._value == value) return ServiceAce;
            if (GuardBreak._value == value) return GuardBreak;
            if (PerfectGame._value == value) return PerfectGame;
            if (WorldQuest._value == value) return WorldQuest;
            if (TotalSmash._value == value) return TotalSmash;
            if (TotalSlice._value == value) return TotalSlice;
            if (TotalChargeShot._value == value) return TotalChargeShot;
            if (TotalLob._value == value) return TotalLob;
            if (TotalServiceAce._value == value) return TotalServiceAce;
            if (TotalReturnAce._value == value) return TotalReturnAce;
            if (TotalSkillShot._value == value) return TotalSkillShot;
            if (TotalWinCount._value == value) return TotalWinCount;
            if (TotalLoseCount._value == value) return TotalLoseCount;
            if (TotalGuardBreak._value == value) return TotalGuardBreak;
            if (TotalPerfectGame._value == value) return TotalPerfectGame;
            if (TotalFishes._value == value) return TotalFishes;
            if (TotalFruits._value == value) return TotalFruits;
            throw new Exception($"Unknown value for QuestConditionType: {value}");
        }
    }
}
