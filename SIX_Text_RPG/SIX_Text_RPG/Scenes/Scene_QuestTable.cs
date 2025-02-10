using SIX_Text_RPG.Others;

namespace SIX_Text_RPG.Scenes;

internal class Scene_QuestTable : Scene_Base
{
    private readonly List<Item> inventory = GameManager.Instance.Inventory;
    
    private static readonly string[] questDetail_1 =
    {
        "?",
        "?",
        "?",
        "?"
    };
    
    private static readonly string[] questDetail_2 =
    {
        "?",
        "?"
    };
    

    public override void Awake()
    {
        base.Awake();
        sceneTitle = "Quest!!";
        sceneInfo = " 퀘스트 테이블 출력하는 장소 / ";
        
        //test용 퀘스트생성
        Item item = new Potion("최대 체력증가 엘릭서", "최대 체력이 20만큼 증가합니다..", 0f, 20f, 0f, 0f, 0, 0, 0);
        Quest quest1 = new Quest(0, "튜터님들 순회공연",questDetail_1,"튜터님들 5명 사냥", 0, item,1000);
        Quest quest2 = new Quest(1, "아이템 수집",questDetail_2,"읎어요", 3, item,0);
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