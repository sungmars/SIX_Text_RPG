using SIX_Text_RPG.Others;

namespace SIX_Text_RPG.Scenes;

internal class Scene_QuestTable : Scene_Base
{
    private readonly List<Item> inventory = GameManager.Instance.Inventory;
    
    //test용 퀘스트생성
    Item item = new Armor(Define.ITEM_INFOS[3,4]);
    private Item potion = new Potion(Define.ITEM_INFOS[2, 1]);
    
    private static readonly string[] questDetail_1 =
    {
        $"안녕하세요 (신조차 모독하는 사상 최강의 {GameManager.Instance.Player.Type}인 {GameManager.Instance.Player.Stats.Name})",
        "질문이 있는데요 (이제부터 내 질문을 답하는데 애로사항이 꽃필것이다!)",
        "또 오류가 났어요 (할수있다 나라면!)",
        "내가 하늘에 서겠다. (머지가 머지)"
    };
    
    private static readonly string[] questDetail_2 =
    {
        "과제 때문에... 힘이 빠진다...",
        "히히히 세상은 다 똥이야 찌르기 발사!"
    };
    
    private static readonly string[] questDetail_3 =
    {
        "아아 이런데서 쓰고 싶지 않았는데",
        $"똑똑히 봐둬라 그리고 아무한테도 말하지 마라 {Define.ITEM_INFOS[3,4].Name}!"
    };
    

    public override void Awake()
    {
        base.Awake();
        sceneTitle = "스파게티 스크럼";
        sceneInfo = $" {GameManager.Instance.Player.Stats.Name}의 학습공간  ";
       
        Quest quest1 = new Quest(0, "노려라 오늘의 질문왕", questDetail_1,
            "튜터님들 5명에게 질문공세로 혼을 쏙 빼놓아라", 5, item, 100);
        Quest quest2 = new Quest(1, "찌르기 벨튀",questDetail_2,"다른 튜터님들 x명 찔러보기", 5, potion,0);
        Quest quest3 = new Quest(2, "찌르기 장착해보기",questDetail_3,"특수아이템 어쩌고 저쩌고", 1, item,0);
        QuestManager.AddQuest(quest1);
        QuestManager.AddQuest(quest2);
        QuestManager.AddQuest(quest3);

        for (int i = 0; i < QuestManager.Quests.Count; i++)
        {
            Menu.Add($"{QuestManager.Instance.QuestFind(i).Name} \n {QuestManager.Instance.QuestFind(i).GoalInfo} \n");
        }
        
        //Menu.Add($"{QuestManager.Instance.QuestFind(0).Name} \n 퀘스트 내용요약 \n");
        //Menu.Add($"{QuestManager.Instance.QuestFind(1).Name} \n 퀘스트 내용요약 \n");
        //Menu.Add($"{QuestManager.Instance.QuestFind(2).Name} \n 퀘스트 내용요약 \n");
         
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
    }
    public void QuestItemCheck()
    {
        var item = inventory.Find(x => (x.Iteminfo.Name == $"{this.item.Iteminfo.Name}"));
        
        if (item is { Iteminfo.IsEquip: true })
        {
            QuestManager.Instance.UpdateQuestProgress(2,1);
        }
        
    }
    
}