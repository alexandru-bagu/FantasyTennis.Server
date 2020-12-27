using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using FTServer.Resources.EmblemQuest;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Resources.Stores
{
    public class EmblemQuestDataStore : IEmblemQuestDataStore
    {
        private const string Resource = "Res/Script/ETC/EmblemQuest.xml";

        private Dictionary<int, Quest> _quests;
        private Dictionary<int, QuestDetail> _questDetails;
        private List<RewardItem> _rewards;
        public IReadOnlyDictionary<int, Quest> Quests => _quests;
        public IReadOnlyDictionary<int, QuestDetail> QuestDetails => _questDetails;
        public IReadOnlyCollection<RewardItem> Rewards => _rewards;

        public EmblemQuestDataStore(IResourceManager resourceManager, ITextDataStore textDataStore)
        {
            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));

            _quests = new Dictionary<int, Quest>();
            _questDetails = new Dictionary<int, QuestDetail>();
            _rewards = new List<RewardItem>();

            loadQuests(textDataStore, resource);
            loadQuestDetails(resource);
            loadRewards(resource);
        }

        private void loadRewards(dynamic resource)
        {
            foreach (dynamic rewardRes in resource.RewardItemNIKIList)
            {
                var reward = new RewardItem();
                loadReward(reward, rewardRes, HeroType.Niki);
                _rewards.Add(reward);
            }
        }

        private void loadReward(RewardItem reward, dynamic rewardRes, HeroType hero)
        {
            reward.Index = rewardRes.Index;
            reward.Hero = hero;
            reward.Name = rewardRes.Name;
            reward.ShopIndex = rewardRes.ShopIndex;
            reward.QuantityMin = rewardRes.QuantityMin;
            reward.QuantityMax = rewardRes.QuantityMax;
        }

        private void loadQuestDetails(dynamic resource)
        {
            foreach (dynamic questDetailRes in resource.QuestDetailList)
            {
                var questDetail = new QuestDetail();
                questDetail.Index = questDetailRes.Index;
                questDetail.GameMode = GameMode.Parse(questDetailRes.GameMode);
                questDetail.LevelRestriction = questDetailRes.LevelRestriction;

                int conditionIndex = 1;
                while (questDetailRes.Members.ContainsKey("ConditionType" + conditionIndex))
                {
                    var condition = new QuestCondition(QuestConditionType.Parse(questDetailRes.Members["ConditionType" + conditionIndex]),
                        ((string)questDetailRes.Members["ConditionCount" + conditionIndex]).Split(',').Select(int.Parse).ToList());
                    questDetail.Conditions.Add(condition);
                    conditionIndex++;
                }

                int requireItemIndex = 1;
                while (questDetailRes.Members.ContainsKey("RequireItem" + requireItemIndex))
                {
                    var requirement = new QuestItemRequirement(questDetailRes.Members["RequireItem" + requireItemIndex], questDetailRes.Members["Quantity" + requireItemIndex]);
                    questDetail.ItemRequirement.Add(requirement);
                    requireItemIndex++;
                }
                _questDetails.Add(questDetail.Index, questDetail);
            }
        }

        private void loadQuests(ITextDataStore textDataStore, dynamic resource)
        {
            foreach (dynamic questRes in resource.QuestList)
            {
                var quest = new Quest();
                quest.Index = questRes.Index;
                quest.Enable = questRes.Enable == "1";
                quest.Event = questRes.Event == "1";
                quest.NameLabel = textDataStore[questRes.QuestNameLabel];
                quest.SuccessCondition = textDataStore[questRes.SuccessCondition];
                quest.QuestDetail = questRes.QuestDetail;
                quest.QuestPreRequisite = ((string)questRes.QuestPreReq).Split(',').Select(int.Parse).ToList();
                quest.RewardExperience = questRes.RewardEXP;
                quest.RewardGold = questRes.RewardGOLD;
                quest.QuestRepeat = questRes.QuestRepeat != "No";
                quest.ItemRewardRepeat = questRes.ItemRewardRepeat != "No";
                if (questRes.Members.ContainsKey("EmblemGrade"))
                    quest.EmblemGrade = questRes.EmblemGrade;

                int rewardIndex = 1;
                while (questRes.Members.ContainsKey("RewardItem" + rewardIndex))
                {
                    quest.RewardItems.Add(questRes.Members["RewardItem" + rewardIndex]);
                    rewardIndex++;
                }
                _quests.Add(quest.Index, quest);
            }
        }
    }
}
