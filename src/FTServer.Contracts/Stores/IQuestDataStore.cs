﻿using FTServer.Resources.Quest;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface IQuestDataStore
    {
        IReadOnlyCollection<Global> Global { get; }
        IReadOnlyDictionary<int, Tutorial> Tutorials { get; }
        IReadOnlyDictionary<int, Challenge> Challenges { get; }
        IReadOnlyDictionary<int, GuardianChallenge> GuardianChallenges { get; }
        IReadOnlyDictionary<int, MiniGame> MiniGames { get; }
        IReadOnlyDictionary<int, PlayerNpc> PlayerNpcs { get; }
        IReadOnlyDictionary<int, MonsterNpc> MonsterNpcs { get; }
    }
}
