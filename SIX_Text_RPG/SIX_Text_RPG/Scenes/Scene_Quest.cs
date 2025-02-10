using SIX_Text_RPG.Others;

namespace SIX_Text_RPG.Scenes;

internal class Scene_Quest : Scene_Base
{
    private Quest quest;
    public Item reward;

    public Scene_Quest(int questId)
    {
        quest = QuestManager.Instance.QuestFind(questId);
    }
    
    public override void Awake()
    {
        sceneTitle = "Quest!!";
        switch (quest.Id)
        {
            case 0 :
                sceneInfo = $" {quest.Name} (튜터님들 몇명 쓰러트린지)";
                break;
            case 1 :
                sceneInfo = " 퀘스트 제목 2 / 오늘의 찌르기왕 (특수 아이템 장비 확인?)";
                break;
        }
        
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
                    Console.WriteLine("퀘스트 완료 o");
                    // GameManager.Instance.Inventory + reward; 보상

                }
                else if (quest.Status == QuestStatus.InProgress)
                {
                    Console.WriteLine("퀘스트 완료 x");
                }
                else if(quest.Status == QuestStatus.NotStarted)
                {
                    Console.WriteLine("퀘스트 수락완료");
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
        if (quest.Id == 0)
        {
            Console.WriteLine("퀘스트 내용");
            Console.WriteLine("");

            Console.WriteLine("퀘스트 완료조건");
            Console.WriteLine("");

            Console.WriteLine("보상");
            Console.WriteLine("");
        }
        
        else if (quest.Id == 1)
        {
            Console.WriteLine("퀘스트 내용");
            Console.WriteLine("");

            Console.WriteLine("퀘스트 완료조건");
            Console.WriteLine("");

            Console.WriteLine("보상");
            Console.WriteLine("");
        }
    }
}