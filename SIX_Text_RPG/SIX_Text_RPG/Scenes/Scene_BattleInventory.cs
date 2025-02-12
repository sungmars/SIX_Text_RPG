namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleInventory : Scene_Battle
    {
        List<Item> playerInventory = GameManager.Instance.Inventory;
        public List<Item> Potion
        {
            get { return playerInventory.Where(item => item is IConsumable).ToList(); }
        }

        private readonly int CURSOR_MENU_X = 30;
        private readonly int CURSOR_MENU_Y = 23;

        public override void Awake()
        {
            hasZero = false;

            for (int i = 0; i < Potion.Count; i++)
            {
                var selectItem = Potion[i];
                string itemName = selectItem.Iteminfo.Name;
                // 앞쪽에서 같은 이름의 아이템이 출력되었는지 확인하기 위해 bool선언
                bool alreadyPrint = false;
                for (int j = 0; j < i; j++)//j는 위의 for문 i까지
                {
                    if (Potion[j].Iteminfo.Name == itemName)
                    {
                        alreadyPrint = true;
                        break;
                    }
                }
                if (alreadyPrint)
                {
                    continue;
                }//출력됐다면 다음 아이템출력으로 넘어가기하기
                int count = playerInventory.Count(item => item is IConsumable && item.Iteminfo.Name == itemName);//중복개수 세기
                string displayName = selectItem.Iteminfo.Name +
                    (count != 0 ? $" -({count})" : "이게 보인다면 버그입니다.");//아이템 갯수 표시
                if(!displayName.Contains("최대"))
                {
                    Utils.CursorMenu.Add((displayName, () =>
                    {
                        for (int i = 0; i < Utils.CursorMenu.Count; i++)
                        {
                            Utils.ClearLine(CURSOR_MENU_X - 3, CURSOR_MENU_Y + i);
                        }
                        Utils.CursorMenu.Clear();
                        UseItem(playerInventory.FindIndex(x => x.Equals(selectItem)));
                        Program.CurrentScene = new Scene_BattleInventory();
                    }
                    ));
                }
            }

            // 가방 닫기시 메뉴 다 지우고 로비로 돌아가기
            Utils.CursorMenu.Add(("가방 닫기", () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Utils.ClearLine(0, CURSOR_MENU_Y + i);
                }
                Utils.CursorMenu.Clear();
                Program.CurrentScene = new Scene_BattleLobby();
            }
            ));
        }

        public override void LateStart()
        {
            Utils.DisplayCursorMenu(CURSOR_MENU_X, CURSOR_MENU_Y);
        }

        public override int Update()
        {
            if(base.Update() == 0)
            {
                return 0;
            }
            else
            {

            }
            return 1;
        }

        /*protected override void Display()
        {
            base.Display();
        }*/


        public void UseItem(int j)
        {
            var selectItem = playerInventory[j];//캡쳐 방지
            IConsumable? consumable = selectItem as IConsumable;

            if (consumable != null && consumable.Consume())//소비하는 아이템이라면
            {
                playerInventory.Remove(selectItem);//인벤토리 리스트에서 삭제
            }
            else
            {
                Utils.WriteColorLine("버그", ConsoleColor.Red);
                Console.ReadKey(); // 대기
            }
        }
    }
}