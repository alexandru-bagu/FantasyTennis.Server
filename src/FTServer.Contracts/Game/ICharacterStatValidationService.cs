namespace FTServer.Contracts.Game
{
    public interface ICharacterStatValidationService
    {
        bool IsValid(CharacterType type, byte level, byte strength, byte stamina, byte dexterity, byte willpower, byte statPoints);
    }
}
