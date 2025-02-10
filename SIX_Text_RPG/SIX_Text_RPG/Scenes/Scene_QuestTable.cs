using SIX_Text_RPG.Others;

namespace SIX_Text_RPG.Scenes;

internal class Scene_QuestTable : Scene_Base
{
    private List<Item> inventory = GameManager.Instance.Inventory;

    public override void Awake()
    {
        sceneTitle = "Quest!!";
        sceneInfo = " 퀘스트 테이블 출력하는 장소 / ";
        
        //test용 퀘스트생성
        Quest quest1 = new Quest(0, "튜터님들 순회공연", 5);
        Quest quest2 = new Quest(1, "아이템 수집", 3);
        QuestManager.AddQuest(quest1);
        QuestManager.AddQuest(quest2);
        
        
        Menu.Add($"{QuestManager.Instance.QuestFind(0).Name} \n 퀘스트 내용요약 \n");
        Menu.Add($"{QuestManager.Instance.QuestFind(1).Name} \n 퀘스트 내용요약 \n");
        
        //Quest1();
        //Quest2();
        
       
        
    }

    public override int Update()
    {
        switch (base.Update())
        {
            case 1:
                Program.CurrentScene = new Scene_Quest(0);
                break;
            case 2:
                Program.CurrentScene = new Scene_Quest(1);
                break;
            case 0:
                Program.CurrentScene = new Scene_Lobby();
                break;
        }
        
        return 0;
    }
    
    protected override void Display()
    {
    }

    public void Quest1()
    {
        //test용 퀘스트조건
        if (GameManager.Instance.Player.Stats.Level == 10)
            QuestManager.Instance.clearQuestId[0] = true;
        
        QuestManager.Instance.UpdateQuestProgress(0,100);
    }
    public void Quest2()
    {
        var item = inventory.Find(x => (x.Iteminfo.Name == "찾는물건"));
        
        

        if (item.Iteminfo.IsSold)   // 장비했는지 확인 지금은 isSold로 오류만 안나게
        {
            QuestManager.Instance.UpdateQuestProgress(1,100);
        }
        
    }
    
}