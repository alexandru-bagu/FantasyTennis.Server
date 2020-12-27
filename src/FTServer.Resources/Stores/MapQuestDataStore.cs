using Dandraka.XmlUtilities;
using FTServer.Contracts.Resources;
using FTServer.Contracts.Stores;
using FTServer.Resources.MapQuest;
using System.Collections.Generic;
using System.Linq;

namespace FTServer.Resources.Stores
{
    public class MapQuestDataStore : IMapQuestDataStore
    {
        private const string Resource = "Res/Script/PubETC/Ini3/MapQuest.xml";

        private List<Global> _globals;
        private Dictionary<ushort, Tutorial> _tutorials;
        private Dictionary<ushort, TennisChallenge> _challenges;
        private Dictionary<int, MiniGame> _miniGames;
        private Dictionary<int, PlayerNpc> _playerNpcs;
        private Dictionary<int, MonsterNpc> _monsterNpcs;

        public IReadOnlyCollection<Global> Global => _globals;
        public IReadOnlyDictionary<ushort, Tutorial> Tutorials => _tutorials;
        public IReadOnlyDictionary<ushort, TennisChallenge> Challenges => _challenges;
        public IReadOnlyDictionary<int, MiniGame> MiniGames => _miniGames;
        public IReadOnlyDictionary<int, PlayerNpc> PlayerNpcs => _playerNpcs;
        public IReadOnlyDictionary<int, MonsterNpc> MonsterNpcs => _monsterNpcs;

        public MapQuestDataStore(IResourceManager resourceManager)
        {
            _globals = new List<Global>();
            _tutorials = new Dictionary<ushort, Tutorial>();
            _challenges = new Dictionary<ushort, TennisChallenge>();
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
            foreach (dynamic globalRes in resource.GlobalList)
            {
                var global = new Global();
                loadGlobal(globalRes, global);
                _globals.Add(global);
            }
        }

        private void loadTutorials(dynamic resource)
        {
            foreach (dynamic tutorialRes in resource.TutorialList)
            {
                var tutorial = new Tutorial();
                loadTutorial(tutorialRes, tutorial);
                _tutorials.Add(tutorial.Index, tutorial);
            }
        }

        private void loadChallenges(dynamic resource)
        {
            foreach (dynamic challengeRes in resource.ChallengeList)
            {
                var challenge = new Challenge();
                loadChallenge(challengeRes, challenge);
                _challenges.Add(challenge.Index, challenge);
            }
        }

        private void loadGuardianChallenges(dynamic resource)
        {
            foreach (dynamic challengeRes in resource.ChallengeGuardianList)
            {
                var guardianChallenge = new GuardianChallenge();
                loadGuardianChallenge(challengeRes, guardianChallenge);
                _challenges.Add(guardianChallenge.Index, guardianChallenge);
            }
        }

        private void loadMiniGames(dynamic resource)
        {
            foreach (dynamic miniGameRes in resource.MiniGameList)
            {
                var miniGame = new MiniGame();
                loadMiniGame(miniGameRes, miniGame);
                _miniGames.Add(miniGame.Index, miniGame);
            }
        }

        private void loadPlayerNpcs(dynamic resource)
        {
            foreach (dynamic npcRes in resource.PlayerNPCList)
            {
                var npc = new PlayerNpc();
                loadPlayerNpc(npcRes, npc);
                _playerNpcs.Add(npc.Index, npc);
            }
        }

        private void loadMonsterNpcs(dynamic resource)
        {
            foreach (dynamic npcRes in resource.MonsterNPCList)
            {
                var npc = new MonsterNpc();
                loadMonsterNpc(npcRes, npc);
                _monsterNpcs.Add(npc.Index, npc);
            }
        }


        private static void loadGlobal(dynamic globalRes, Global global)
        {
            global.MapLocation = globalRes.MapLocation;
            global.GameType = QuestGameType.Parse(globalRes.GameType);
            global.StartIndex = globalRes.StartIndex;
            global.EndIndex = globalRes.EndIndex;
            global.IndexCondition = ((string)globalRes.IndexCondition).Split(',').Select(p => int.Parse(p)).ToList();
        }

        private static void loadTutorial(dynamic tutorialRes, Tutorial tutorial)
        {
            tutorial.Index = tutorialRes.Index;
            if (!(tutorial is Challenge) && !(tutorial is GuardianChallenge))
                tutorial.Name = tutorialRes.Name;
            tutorial.RewardExperience = tutorialRes.RewardEXP;
            tutorial.RewardGold = tutorialRes.RewardGOLD;
            tutorial.ItemRewardRepeat = tutorialRes.ItemRewardRepeat != "No";
            tutorial.RewardItem1 = tutorialRes.RewardItem1;
            tutorial.QuantityMin1 = tutorialRes.QuantityMin1;
            tutorial.QuantityMax1 = tutorialRes.QuantityMax1;
            tutorial.RewardItem2 = tutorialRes.RewardItem2;
            tutorial.QuantityMin2 = tutorialRes.QuantityMin2;
            tutorial.QuantityMax2 = tutorialRes.QuantityMax2;
            tutorial.RewardItem3 = tutorialRes.RewardItem3;
            tutorial.QuantityMin3 = tutorialRes.QuantityMin3;
            tutorial.QuantityMax3 = tutorialRes.QuantityMax3;
            tutorial.SuccessCondition = tutorialRes.SuccessCondition;
        }

        private static void loadChallenge(dynamic challengeRes, Challenge challenge)
        {
            loadTutorial(challengeRes, challenge);
            challenge.Level = challengeRes.Level;
            challenge.LevelRestriction = challengeRes.LevelRestriction;
            challenge.GameMode = GameMode.Parse(challengeRes.GameMode);
            challenge.TennisCourt = challengeRes.TennisCourt;
            challenge.NpcIndex = challengeRes.NpcIndex;
            challenge.AILevel = challengeRes.NpcAILevel;
            challenge.Hitpoints = challengeRes.HP;
            challenge.Strength = (byte)((int)challengeRes.STR);
            challenge.Stamina = (byte)((int)challengeRes.STA);
            challenge.Dexterity = (byte)((int)challengeRes.DEX);
            challenge.Willpower = (byte)((int)challengeRes.WIL);
        }

        private void loadGuardianChallenge(dynamic challengeRes, GuardianChallenge guardianChallenge)
        {
            loadTutorial(challengeRes, guardianChallenge);
            guardianChallenge.Level = challengeRes.Level;
            guardianChallenge.LevelRestriction = challengeRes.LevelRestriction;
            guardianChallenge.GameMode = GameMode.Parse(challengeRes.GameMode);
            guardianChallenge.TennisCourt = challengeRes.TennisCourt;
            guardianChallenge.NpcIndex1 = challengeRes.NpcIndex1;
            guardianChallenge.NpcIndex2 = challengeRes.NpcIndex2;
            guardianChallenge.NpcIndex3 = challengeRes.NpcIndex3;
        }

        private void loadMiniGame(dynamic miniGameRes, MiniGame miniGame)
        {
            miniGame.Index = miniGameRes.Index;
            miniGame.Level = miniGameRes.Level;
            miniGame.LevelRestriction = miniGameRes.LevelRestriction;
            miniGame.GameMode = GameMode.Parse(miniGameRes.GameMode);
        }

        private void loadPlayerNpc(dynamic npcRes, PlayerNpc npc)
        {
            npc.Index = npcRes.Index;
            npc.Model = npcRes.NpcModel;
            npc.Hair = npcRes.NpcHAIR;
            npc.Face = npcRes.NpcFACE;
            npc.Upper = npcRes.NpcUPPER;
            npc.Pants = npcRes.NpcPANTS;
            npc.Legs = npcRes.NpcLEGS;
            npc.Foot = npcRes.NpcFOOT;
            npc.Hand = npcRes.NpcHAND;
            npc.Racket = npcRes.NpcRACKET;
            npc.Glasses = npcRes.NpcGLASSES;
            npc.Bag = npcRes.NpcBAG;
            npc.Cap = npcRes.NpcCAP;
            npc.Dye = npcRes.NpcDYE;
        }
        private void loadMonsterNpc(dynamic npcRes, MonsterNpc npc)
        {
            npc.Index = npcRes.Index;
            npc.Model = npcRes.NpcModel;
            npc.Skin = npcRes.NpcSkin;
            npc.AILevel = npcRes.NpcAILevel;
            npc.Hitpoints = npcRes.HP;
            npc.Strength = (byte)((int)npcRes.STR);
            npc.Stamina = (byte)((int)npcRes.STA);
            npc.Dexterity = (byte)((int)npcRes.DEX);
            npc.Willpower = (byte)((int)npcRes.WIL);
        }
    }
}
