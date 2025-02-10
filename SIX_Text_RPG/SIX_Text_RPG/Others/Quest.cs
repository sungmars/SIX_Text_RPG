namespace SIX_Text_RPG.Others
{
    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Completed
    }

    internal class Quest
    {
        public int Id { get; set; } // 퀘스트 ID
        public string Name { get; set; } // 퀘스트 이름
        public string[] QuestInfo { get; set; } // 퀘스트 내용
        public string GoalInfo { get; set; }    // 퀘스트 목적조건
        public QuestStatus Status { get; private set; } // 퀘스트 상태
        public int CurrentProgress { get; private set; } // 현재 진행도
        public int Goal { get; set; } // 목표 진행도
        public Item ItemReward { get; set; }
        public int GoldReward { get; set; }

        public Quest(int id, string name, string[] questInfo, string goalInfo, int goal, Item itemReward, int goldReward)
        {
            Id = id;
            Name = name;
            QuestInfo = questInfo;
            GoalInfo = goalInfo;
            Status = QuestStatus.NotStarted;
            CurrentProgress = 0;
            Goal = goal;
            ItemReward = itemReward;
            GoldReward = goldReward;
        }

        public void QuestStart()
        {
            if (Status == QuestStatus.NotStarted)
            {
                Status = QuestStatus.InProgress;
            }
        }

        public void QuestProgress(int amount)
        {
            if (Status == QuestStatus.InProgress)
            {
                CurrentProgress += amount;
            }
        }
        
        public void CompleteQuest()
        {
            if (Status == QuestStatus.InProgress)
            {
                if (CurrentProgress >= Goal)
                    Status = QuestStatus.Completed;
            }
        }
    }
    
    
}