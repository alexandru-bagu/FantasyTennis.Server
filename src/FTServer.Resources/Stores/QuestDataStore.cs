using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using FTServer.Resources.Quest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Resources.Stores
{
    public class QuestDataStore : IQuestDataStore
    {
        private const string Resource = "Res/Script/PubETC/Ini3/MapQuest.xml";

        private List<Global> _globals;
        private Dictionary<int, Tutorial> _tutorials;
        private Dictionary<int, Challenge> _challenges;
        private Dictionary<int, GuardianChallenge> _guardianChallenges;
        private Dictionary<int, MiniGame> _miniGames;
        private Dictionary<int, PlayerNpc> _playerNpcs;
        private Dictionary<int, MonsterNpc> _monsterNpcs;

        public IReadOnlyCollection<Global> Global => _globals;
        public IReadOnlyDictionary<int, Tutorial> Tutorials => _tutorials;
        public IReadOnlyDictionary<int, Challenge> Challenges => _challenges;
        public IReadOnlyDictionary<int, GuardianChallenge> GuardianChallenges => _guardianChallenges;
        public IReadOnlyDictionary<int, MiniGame> MiniGames => _miniGames;
        public IReadOnlyDictionary<int, PlayerNpc> PlayerNpcs => _playerNpcs;
        public IReadOnlyDictionary<int, MonsterNpc> MonsterNpcs => _monsterNpcs;

        public QuestDataStore(IResourceManager resourceManager)
        {
            _globals = new List<Global>();
            _tutorials = new Dictionary<int, Tutorial>();
            _challenges = new Dictionary<int, Challenge>();
            _guardianChallenges = new Dictionary<int, GuardianChallenge>();
            _miniGames = new Dictionary<int, MiniGame>();
            _playerNpcs = new Dictionary<int, PlayerNpc>();
            _monsterNpcs = new Dictionary<int, MonsterNpc>();

            var resource = XmlSlurper.ParseText(resourceManager.ReadResource(Resource));
            loadGlobals(resource);
            loadTutorials(resource);
            loadChallenges(resource);
            loadGuardianChallenges(resource);
            loadMiniGames(resource);
            loadPlayerNpcs(resource);
            loadMonsterNpcs(resource);
        }

        private void loadGlobals(dynamic resource)
        {
            foreach (dynamic global in resource.GlobalList)
            {
                var questGlobal = new Global();
                loadGlobal(global, questGlobal);
                _globals.Add(questGlobal);
            }
        }

        private void loadTutorials(dynamic resource)
        {
            foreach (dynamic tutorial in resource.TutorialList)
            {
                var questTutorial = new Tutorial();
                loadTutorial(tutorial, questTutorial);
                _tutorials.Add(questTutorial.Index, questTutorial);
            }
        }

        private void loadChallenges(dynamic resource)
        {
            foreach (dynamic challenge in resource.ChallengeList)
            {
                var questChallenge = new Challenge();
                loadChallenge(challenge, questChallenge);
                _challenges.Add(questChallenge.Index, questChallenge);
            }
        }

        private void loadGuardianChallenges(dynamic resource)
        {
            foreach (dynamic challenge in resource.ChallengeGuardianList)
            {
                var questGuardianChallenge = new GuardianChallenge();
                loadGuardianChallenge(challenge, questGuardianChallenge);
                _guardianChallenges.Add(questGuardianChallenge.Index, questGuardianChallenge);
            }
        }

        private void loadMiniGames(dynamic resource)
        {
            foreach (dynamic miniGame in resource.MiniGameList)
            {
                var questMiniGame = new MiniGame();
                loadMiniGame(miniGame, questMiniGame);
                _miniGames.Add(questMiniGame.Index, questMiniGame);
            }
        }

        private void loadPlayerNpcs(dynamic resource)
        {
            foreach (dynamic npc in resource.PlayerNPCList)
            {
                var playerNpc = new PlayerNpc();
                loadPlayerNpc(npc, playerNpc);
                _playerNpcs.Add(playerNpc.Index, playerNpc);
            }
        }

        private void loadMonsterNpcs(dynamic resource)
        {
            foreach (dynamic npc in resource.MonsterNPCList)
            {
                var monsterNpc = new MonsterNpc();
                loadMonsterNpc(npc, monsterNpc);
                _monsterNpcs.Add(monsterNpc.Index, monsterNpc);
            }
        }


        private static void loadGlobal(dynamic global, Global questGlobal)
        {
            questGlobal.MapLocation = global.MapLocation;
            questGlobal.GameType = QuestGameType.Parse(global.GameType);
            questGlobal.StartIndex = global.StartIndex;
            questGlobal.EndIndex = global.EndIndex;
            questGlobal.IndexCondition = ((string)global.IndexCondition).Split(',').Select(p => int.Parse(p)).ToList();
        }

        private static void loadTutorial(dynamic tutorial, Tutorial questTutorial)
        {
            questTutorial.Index = tutorial.Index;
            if (!(questTutorial is Challenge) && !(questTutorial is GuardianChallenge))
                questTutorial.Name = tutorial.Name;
            questTutorial.RewardExperience = tutorial.RewardEXP;
            questTutorial.RewardGold = tutorial.RewardGOLD;
            questTutorial.ItemRewardRepeat = tutorial.ItemRewardRepeat != "No";
            questTutorial.RewardItem1 = tutorial.RewardItem1;
            questTutorial.QuantityMin1 = tutorial.QuantityMin1;
            questTutorial.QuantityMax1 = tutorial.QuantityMax1;
            questTutorial.RewardItem2 = tutorial.RewardItem2;
            questTutorial.QuantityMin2 = tutorial.QuantityMin2;
            questTutorial.QuantityMax2 = tutorial.QuantityMax2;
            questTutorial.RewardItem3 = tutorial.RewardItem3;
            questTutorial.QuantityMin3 = tutorial.QuantityMin3;
            questTutorial.QuantityMax3 = tutorial.QuantityMax3;
            questTutorial.SuccessCondition = tutorial.SuccessCondition;
        }

        private static void loadChallenge(dynamic challenge, Challenge questChallenge)
        {
            loadTutorial(challenge, questChallenge);
            questChallenge.Level = challenge.Level;
            questChallenge.LevelRestriction = challenge.LevelRestriction;
            questChallenge.GameMode = GameMode.Parse(challenge.GameMode);
            questChallenge.TennisCourt = challenge.TennisCourt;
            questChallenge.NpcIndex = challenge.NpcIndex;
            questChallenge.AILevel = challenge.NpcAILevel;
            questChallenge.Hitpoints = challenge.HP;
            questChallenge.Strength = (byte)((int)challenge.STR);
            questChallenge.Stamina = (byte)((int)challenge.STA);
            questChallenge.Dexterity = (byte)((int)challenge.DEX);
            questChallenge.Willpower = (byte)((int)challenge.WIL);
        }

        private void loadGuardianChallenge(dynamic challenge, GuardianChallenge questGuardianChallenge)
        {
            loadTutorial(challenge, questGuardianChallenge);
            questGuardianChallenge.Level = challenge.Level;
            questGuardianChallenge.LevelRestriction = challenge.LevelRestriction;
            questGuardianChallenge.GameMode = GameMode.Parse(challenge.GameMode);
            questGuardianChallenge.TennisCourt = challenge.TennisCourt;
            questGuardianChallenge.NpcIndex1 = challenge.NpcIndex1;
            questGuardianChallenge.NpcIndex2 = challenge.NpcIndex2;
            questGuardianChallenge.NpcIndex3 = challenge.NpcIndex3;
        }

        private void loadMiniGame(dynamic miniGame, MiniGame questMiniGame)
        {
            questMiniGame.Index = miniGame.Index;
            questMiniGame.Level = miniGame.Level;
            questMiniGame.LevelRestriction = miniGame.LevelRestriction;
            questMiniGame.GameMode = GameMode.Parse(miniGame.GameMode);
        }

        private void loadPlayerNpc(dynamic npc, PlayerNpc playerNpc)
        {
            playerNpc.Index = npc.Index;
            playerNpc.Model = npc.NpcModel;
            playerNpc.Hair = npc.NpcHAIR;
            playerNpc.Face = npc.NpcFACE;
            playerNpc.Upper = npc.NpcUPPER;
            playerNpc.Pants = npc.NpcPANTS;
            playerNpc.Legs = npc.NpcLEGS;
            playerNpc.Foot = npc.NpcFOOT;
            playerNpc.Hand = npc.NpcHAND;
            playerNpc.Racket = npc.NpcRACKET;
            playerNpc.Glasses = npc.NpcGLASSES;
            playerNpc.Bag = npc.NpcBAG;
            playerNpc.Cap = npc.NpcCAP;
            playerNpc.Dye = npc.NpcDYE;
        }
        private void loadMonsterNpc(dynamic npc, MonsterNpc monsterNpc)
        {
            monsterNpc.Index = npc.Index;
            monsterNpc.Model = npc.NpcModel;
            monsterNpc.Skin = npc.NpcSkin;
            monsterNpc.AILevel = npc.NpcAILevel;
            monsterNpc.Hitpoints = npc.HP;
            monsterNpc.Strength = (byte)((int)npc.STR);
            monsterNpc.Stamina = (byte)((int)npc.STA);
            monsterNpc.Dexterity = (byte)((int)npc.DEX);
            monsterNpc.Willpower = (byte)((int)npc.WIL);
        }
    }
}
