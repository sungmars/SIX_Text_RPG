using SIX_Text_RPG.Others;

namespace SIX_Text_RPG

{
    internal class QuestManager
    {
        public static QuestManager Instance { get; set; } = new();

        public static Dictionary<int, Quest> Quests { get; set; } = new Dictionary<int, Quest>();

        // 튜터님들 한명씩 검사


        //test용 퀘스트아이템
        private static readonly ItemInfo info = new()
        {
            Name = "스파게티", Description = "그것은 스파게티라기엔 너무 컸다", ATK = 111, DEF = 0, HP = 0, MaxHP = 0, MP = 0,
            MaxMP = 0, Price = -1, Graphic = '|', Color = ConsoleColor.Gray
        };

        private static readonly Item legendItem = new Weapon(info);
        private static readonly Item potion = new Potion(Define.ITEM_INFOS[2, 1]);
        public static Item equipQuestItem = new Weapon(Define.ITEM_INFOS[3,3]);  // 장착 아이템 퀘스트 define에서 하나 골라서 넣을것
        
        private static readonly string[] questDetail_1 =
        {
            $"안녕하세요 (신조차 모독하는 사상 최강의 {GameManager.Instance.Player.Type}인 {GameManager.Instance.Player.Stats.Name})",
            " 질문이 있는데요 (이제부터 내 질문을 답하는데 애로사항이 꽃필 것이다!)",
            " 또 오류가 났어요 (할수있다 나라면!)",
            " 내가 하늘에 서겠다. (머지가 머지)"
        };
    
        private static readonly string[] questDetail_2 =
        {
            "과제 때문에... 힘이 빠진다...",
            " 히히히 세상은 다 똥이야 찌르기 발사!"
        };
    
        private static readonly string[] questDetail_3 =
        {
            "아아 이런데서 쓰고 싶지 않았는데",
            $" 똑똑히 봐둬라 그리고 아무한테도 말하지 마라 {equipQuestItem.Iteminfo.Name}!"
        };
        

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
                if (!Quests[questId].killCount[MonsterTypeCount] && Quests[questId].Status == QuestStatus.InProgress)
                {
                    UpdateQuestProgress(questId,1);
                    Quests[questId].killCount[MonsterTypeCount] = true;
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
                for (int i = 0; i < Quests[questId].ItemRewardCount; i++)
                {
                    GameManager.Instance.Inventory.Add(Quests[questId].ItemReward);
                }
                
                // Gold 보상
                player.SetStat(Stat.Gold,Quests[questId].GoldReward, true);

            }
        }

        public void QuestInitialize()
        {
            Quest quest1 = new Quest(0, "노려라 오늘의 질문왕", questDetail_1,
                "튜터님들 5명에게 질문공세로 혼을 쏙 빼놓아라", 5, legendItem,1, 500, null);
            Quest quest2 = new Quest(1, "찌르기 벨튀",questDetail_2,"다른 튜터님들 8명 찔러보기", 8, potion,
                4,0, new List<bool>(new bool[(int)MonsterType.Count]));
            Quest quest3 = new Quest(2, "찌르기 장착해보기",questDetail_3,$"장비 \"{equipQuestItem.Iteminfo.Name}\" " +
                                                                  $"장착해보기", 1, potion,2,500,null);
            AddQuest(quest1);
            AddQuest(quest2);
            AddQuest(quest3);
        }
        
    }
}