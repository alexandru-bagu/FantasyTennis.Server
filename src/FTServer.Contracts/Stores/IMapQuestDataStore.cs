using FTServer.Resources.MapQuest;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface IMapQuestDataStore
    {
        IReadOnlyCollection<Global> Global { get; }
        IReadOnlyDictionary<ushort, Tutorial> Tutorials { get; }
        IReadOnlyDictionary<ushort, TennisChallenge> Challenges { get; }
        IReadOnlyDictionary<int, MiniGame> MiniGames { get; }
        IReadOnlyDictionary<int, PlayerNpc> PlayerNpcs { get; }
        IReadOnlyDictionary<int, MonsterNpc> MonsterNpcs { get; }
    }
}
