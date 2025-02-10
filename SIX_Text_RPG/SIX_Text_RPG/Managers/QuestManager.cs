using SIX_Text_RPG.Others;

namespace SIX_Text_RPG

{
    internal class QuestManager
    {
        public static QuestManager Instance { get; set; } = new();

        public static Dictionary<int, Quest> Quests { get; set; } = new Dictionary<int, Quest>();

        // 튜터님들 한명씩 검사?
        public List<bool> killCount = new List<bool>(new bool[(int)MonsterType.Count]);
        //test용 
        public List<bool> clearQuestId { get; set; } = new List<bool>() {true,true};

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
        
        
    }
}