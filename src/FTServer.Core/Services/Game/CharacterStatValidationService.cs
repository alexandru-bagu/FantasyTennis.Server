using FTServer.Contracts.Game;

namespace FTServer.Core.Services.Game
{
    public class CharacterStatValidationService : ICharacterStatValidationService
    {
        public bool IsValid(HeroType type, byte level, byte strength, byte stamina, byte dexterity, byte willpower, byte statPoints)
        {
            if (level != 1) return false;
            if (strength + stamina + dexterity + willpower + statPoints - 60 != 5) return false;
            if (type.Is(HeroType.Niki) || type.Is(HeroType.LunLun))
            {
                if (strength < 15) return false;
                if (stamina < 15) return false;
                if (dexterity < 15) return false;
                if (willpower < 15) return false;
                return true;
            }
            if (type.Is(HeroType.Dennis))
            {
                if (strength < 10) return false;
                if (stamina < 25) return false;
                if (dexterity < 10) return false;
                if (willpower < 15) return false;
                return true;
            }
            if (type.Is(HeroType.Lucy))
            {
                if (strength < 25) return false;
                if (stamina < 15) return false;
                if (dexterity < 5) return false;
                if (willpower < 15) return false;
                return true;
            }
            if (type.Is(HeroType.Shua))
            {
                if (strength < 10) return false;
                if (stamina < 10) return false;
                if (dexterity < 25) return false;
                if (willpower < 15) return false;
                return true;
            }
            if (type.Is(HeroType.Pochi))
            {
                if (strength < 5) return false;
                if (stamina < 15) return false;
                if (dexterity < 15) return false;
                if (willpower < 25) return false;
                return true;
            }
            if (type.Is(HeroType.Al))
            {
                if (strength < 10) return false;
                if (stamina < 5) return false;
                if (dexterity < 20) return false;
                if (willpower < 25) return false;
                return true;
            }
            return false;
        }
    }
}
