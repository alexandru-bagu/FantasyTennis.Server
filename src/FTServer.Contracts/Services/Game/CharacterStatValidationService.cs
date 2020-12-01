using FTServer.Contracts.Game;

namespace FTServer.Contracts.Services.Game
{
    public class CharacterStatValidationService : ICharacterStatValidationService
    {
        public bool IsValid(CharacterType type, byte level, byte strength, byte stamina, byte dexterity, byte willpower, byte statPoints)
        {
            if (level != 1) return false;
            if (strength + stamina + dexterity + willpower + statPoints - 60 != 5) return false;
            if (type == CharacterType.Niki || type == CharacterType.LunLun)
            {
                if (strength < 15) return false;
                if (stamina < 15) return false;
                if (dexterity < 15) return false;
                if (willpower < 15) return false;
                return true;
            }
            if (type == CharacterType.Dennis)
            {
                if (strength < 10) return false;
                if (stamina < 25) return false;
                if (dexterity < 10) return false;
                if (willpower < 15) return false;
                return true;
            }
            if (type == CharacterType.Lucy)
            {
                if (strength < 25) return false;
                if (stamina < 15) return false;
                if (dexterity < 5) return false;
                if (willpower < 15) return false;
                return true;
            }
            if (type == CharacterType.Shua)
            {
                if (strength < 10) return false;
                if (stamina < 10) return false;
                if (dexterity < 25) return false;
                if (willpower < 15) return false;
                return true;
            }
            if (type == CharacterType.Pochi)
            {
                if (strength < 5) return false;
                if (stamina < 15) return false;
                if (dexterity < 15) return false;
                if (willpower < 25) return false;
                return true;
            }
            if (type == CharacterType.Al)
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
