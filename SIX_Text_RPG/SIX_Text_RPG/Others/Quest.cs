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
        public QuestStatus Status { get; set; } // 퀘스트 상태
        public int CurrentProgress { get; set; } // 현재 진행도
        public int Goal { get; set; } // 목표 진행도

        public Quest(int id, string name, int goal)
        {
            Id = id;
            Name = name;
            Status = QuestStatus.NotStarted;
            CurrentProgress = 0;
            Goal = goal;
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

                if (CurrentProgress >= Goal)
                    Status = QuestStatus.Completed;
            }
        }
        
        public void CompleteQuest()
        {
            if (Status == QuestStatus.InProgress)
            {
                Status = QuestStatus.Completed;
            }
        }
        
        
    }
    
    
}