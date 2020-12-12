using FTServer.Resources.EmblemQuest;
using System.Collections.Generic;

namespace FTServer.Contracts.Stores
{
    public interface IEmblemQuestDataStore
    {
        IReadOnlyDictionary<int, Quest> Quests { get; }
        IReadOnlyDictionary<int, QuestDetail> QuestDetails { get; }
        IReadOnlyCollection<RewardItem> Rewards { get; }
    }
}
