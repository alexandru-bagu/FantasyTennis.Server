namespace FTServer.Contracts.Game
{
    public interface ICharacterStatValidationService
    {
        bool IsValid(HeroType type, byte level, byte strength, byte stamina, byte dexterity, byte willpower, byte statPoints);
    }
}
