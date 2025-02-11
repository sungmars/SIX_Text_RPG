using SIX_Text_RPG.Others;

namespace SIX_Text_RPG

{
    internal class QuestManager
    {
        public static QuestManager Instance { get; set; } = new();

        public static Dictionary<int, Quest> Quests { get; set; } = new Dictionary<int, Quest>();

        // 튜터님들 한명씩 검사
        public readonly List<bool> killCount = new List<bool>(new bool[(int)MonsterType.Count]);

        public static void AddQuest(Quest quest)
        {
            if (!Quests.ContainsKey(quest.Id))
            {
                Quests.Add(quest.Id, quest);
            }
        }
        
        public void QuestStart(int questId)
        {
            if (Quests.ContainsKey(questId))
            {
                Quests[questId].QuestStart();
            }
        }
        
        public void UpdateQuestProgress(int questId, int amount)
        {
            if (Quests.ContainsKey(questId))
            {
                Quests[questId].QuestProgress(amount);
            }
        }
        
        public Quest QuestFind(int questId)
        {
            if (Quests.ContainsKey(questId))
            {
                return Quests[questId];
            }

            return null;
        }

        public bool KillCountPlus(int questId, int MonsterTypeCount)
        {
            if (Quests.ContainsKey(questId))
            {
                if (!killCount[MonsterTypeCount] && Quests[questId].Status == QuestStatus.InProgress)
                {
                    UpdateQuestProgress(questId,1);
                    killCount[MonsterTypeCount] = true;
                    return true;
                }
            }

            return false;
        }

        public void QuestReward(Player player, int questId)
        {
            if (Quests.ContainsKey(questId))
            {
                // 플레이어한테 아이템보상
                GameManager.Instance.Inventory.Add(Quests[questId].ItemReward);
                
                // Gold 보상
                var playerStats = player.Stats;
                playerStats.Gold += Quests[questId].GoldReward;
                player.Stats = playerStats;

            }
        }
        
    }
}