using FTServer.Database.Model;
using System.Collections.Generic;

namespace FTServer.Contracts.Game
{
    public interface ICharacterEquipmentBuilder
    {
        Equipment Generate(IEnumerable<Item> items);
    }
}
