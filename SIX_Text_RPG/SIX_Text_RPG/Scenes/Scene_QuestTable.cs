using SIX_Text_RPG.Others;

namespace SIX_Text_RPG.Scenes;

internal class Scene_QuestTable : Scene_Base
{
    private readonly List<Item> inventory = GameManager.Instance.Inventory;

    public override void Awake()
    {
        base.Awake();
        sceneTitle = "스파게티 스크럼";
        sceneInfo = $"{GameManager.Instance.Player.Stats.Name}의 학습공간  ";
       
        

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
        // var quest = QuestManager.Instance.QuestFind(2);
        var item = inventory.Find(x => (x.Iteminfo.Name == $"{QuestManager.equipQuestItem.Iteminfo.Name}"));
        
        if (item is { Iteminfo.IsEquip: true })
        {
            QuestManager.Instance.UpdateQuestProgress(2,1);
        }
        
    }
    
}