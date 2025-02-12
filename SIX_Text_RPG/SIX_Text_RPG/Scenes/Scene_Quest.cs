using SIX_Text_RPG.Others;

namespace SIX_Text_RPG.Scenes;

internal class Scene_Quest : Scene_Base
{
    private Quest quest;
    public Scene_Quest(int questId)
    {
        quest = QuestManager.Instance.QuestFind(questId);
    }
    
    public override void Awake()
    {
        base.Awake();
        sceneTitle = "스파게티 스크럼";
        sceneInfo = $" {quest.Name} ";
        hasZero = false;
        
        // 퀘스트 받지 않았을때
        if (quest.Status == QuestStatus.NotStarted)
        {
            Menu.Add("수락");
            Menu.Add("거절");
        }
        else
        {
            Menu.Add("보상받기");
            Menu.Add("돌아가기");
        }

    }

    public override int Update()
    {
        switch (base.Update())
        {
            case 1:
                if (quest.Status == QuestStatus.Completed)
                {
                    Utils.WriteAnim("이미 퀘스트 완료했습니다");
                }
                else if (quest.Status == QuestStatus.InProgress)
                {
                    quest.CompleteQuest();
                    if (quest.Status == QuestStatus.InProgress)
                    {
                        Utils.WriteAnim("퀘스트 조건을 만족하지 못했습니다");
                    }
                    else if(quest.Status == QuestStatus.Completed)
                    {
                        Utils.WriteAnim("퀘스트를 완료했습니다");
                        
                        if (quest.GoldReward > 0)
                        {
                            GameManager.Instance.Player.DisplayInfo_Gold();
                            GameManager.Instance.Player.StatusAnim(Stat.Gold, quest.GoldReward);
                        }
                        
                        // 퀘스트 보상 넣기
                        if (GameManager.Instance.Player != null)
                            QuestManager.Instance.QuestReward(GameManager.Instance.Player, quest.Id);
                       
                    }
                }
                else if(quest.Status == QuestStatus.NotStarted)
                {
                    Utils.WriteAnim("퀘스트 수락완료");
                    QuestManager.Instance.QuestStart(quest.Id);
                }
                 
                Program.CurrentScene = new Scene_QuestTable();
                break;
            case 2:
                Program.CurrentScene = new Scene_QuestTable();
                break;
        }
        
        return 0;
    }
    
    protected override void Display()
    {
        foreach (var questInfo in quest.QuestInfo)
        {
            Utils.WriteColorLine(questInfo,ConsoleColor.Green);
        }
        Console.WriteLine("\n 퀘스트 완료조건");
        Utils.WriteAnim($"{quest.GoalInfo} ({quest.CurrentProgress}/{quest.Goal})",ConsoleColor.Yellow);
        
        if(quest.Id == 1)
            for (int i = 0; i < (int)MonsterType.Count; i++)
            {
                string name = Enum.GetName(typeof(MonsterType), i);
                Utils.WriteAnim($"{name,5}",ConsoleColor.Yellow);
                Console.SetCursorPosition(8,14+i);
                if(QuestManager.Instance.killCount[i])
                    Utils.WriteColorLine($"     잡았다요놈",ConsoleColor.Blue);
                else
                {
                    Utils.WriteColorLine($"     꼭꼭숨어라",ConsoleColor.Red);
                }
               
            }
        
        Console.WriteLine("\n 보상");
        Utils.WriteColor($"{quest.ItemReward.Iteminfo.Name}",ConsoleColor.Cyan);
        if(quest.ItemRewardCount > 1)
            Utils.WriteColor($"x{quest.ItemRewardCount}  ",ConsoleColor.Cyan);
        if(quest.GoldReward > 0)
            Utils.WriteColor($"    {quest.GoldReward}G  ",ConsoleColor.Cyan);
        Console.WriteLine("");
    }
}