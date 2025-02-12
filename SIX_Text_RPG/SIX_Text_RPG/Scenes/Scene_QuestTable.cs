using SIX_Text_RPG.Others;

namespace SIX_Text_RPG.Scenes;

internal class Scene_QuestTable : Scene_Base
{
    private readonly List<Item> inventory = GameManager.Instance.Inventory;
    
    //test용 퀘스트아이템
    private static readonly ItemInfo info = new()
    {
        Name = "스파게티", Description = "그것은 스파게티라기엔 너무 컸다", ATK = 111, DEF = 0, HP = 0, MaxHP = 0, MP = 0,
        MaxMP = 0, Price = -1, Graphic = '|', Color = ConsoleColor.Gray
    };
    readonly Item item = new Weapon(info);
    private readonly Item potion = new Potion(Define.ITEM_INFOS[2, 1]);
    private static Item equipQuestItem = new Weapon(Define.ITEM_INFOS[3,3]);  // 장착 아이템 퀘스트 define에서 하나 골라서 넣을것
    
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
    

    public override void Awake()
    {
        base.Awake();
        sceneTitle = "스파게티 스크럼";
        sceneInfo = $"{GameManager.Instance.Player.Stats.Name}의 학습공간  ";
       
        Quest quest1 = new Quest(0, "노려라 오늘의 질문왕", questDetail_1,
            "튜터님들 5명에게 질문공세로 혼을 쏙 빼놓아라", 5, item,5, 500);
        Quest quest2 = new Quest(1, "찌르기 벨튀",questDetail_2,"다른 튜터님들 8명 찔러보기", 8, potion,4,0);
        Quest quest3 = new Quest(2, "찌르기 장착해보기",questDetail_3,$"장비 \"{equipQuestItem.Iteminfo.Name}\" " +
                                                              $"장착해보기", 1, potion,2,500);
        QuestManager.AddQuest(quest1);
        QuestManager.AddQuest(quest2);
        QuestManager.AddQuest(quest3);

        for (int i = 0; i < QuestManager.Quests.Count; i++)
        {
            Menu.Add($"{QuestManager.Instance.QuestFind(i).Name} \n {QuestManager.Instance.QuestFind(i).GoalInfo} \n \n ");
        }
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
            case 3:
                QuestItemCheck();
                Program.CurrentScene = new Scene_Quest(2);
                break;
            case 0:
                Program.CurrentScene = new Scene_Lobby();
                break;
        }
        
        return 0;
    }
    
    protected override void Display()
    {
        Console.SetCursorPosition(1, 7);
        
    }
    public void QuestItemCheck()
    {
        var item = inventory.Find(x => (x.Iteminfo.Name == $"{equipQuestItem.Iteminfo.Name}"));
        
        if (item is { Iteminfo.IsEquip: true })
        {
            QuestManager.Instance.UpdateQuestProgress(2,1);
        }
        
    }
    
}