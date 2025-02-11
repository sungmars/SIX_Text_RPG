using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{

    internal class Scene_Inventory : Scene_Base
    {
        private List<Item> Inventory => GameManager.Instance.Inventory;
        public List<Item> Potion
        {
             get {return Inventory.Where(item => item is IConsumable).ToList(); }
        }
        public override void Awake()
        {
            base.Awake();
            sceneTitle = "인벤토리";
            sceneInfo = "Enter키를 눌러 장착/해제 가능합니다.";
        }
        protected override void Display()
        {

            Console.SetCursorPosition(0, 10);
            if (Inventory.Count == 0)//아이템이 없다면 아무것도 없다는 문장과 엔터키를 누르면 나가는 기능
            {
                Utils.WriteColorLine("     현재 가방에 있는 것: 공기", ConsoleColor.DarkGray);
                Utils.CursorMenu.Add((
                        "나가려면 Enter키를 누르세요.", // 메뉴에 출력될 아이템 이름
                        () =>
                        {
                            Program.CurrentScene = new Scene_Lobby();
                        }
                ));
                Utils.DisplayCursorMenu(5, 15);
            }
            else
            {
                // 장비아이템, 소모아이템 필터링
                var equipItems = Inventory.Where(item => item is IEquipable).ToList();
                //장비 아이템 출력
                Utils.WriteColorLine("장비가능한 아이템", ConsoleColor.DarkYellow);
                for (int i = 0; i < equipItems.Count; i++)
                {
                    var selectItem = equipItems[i];
                    string displayName = selectItem.Iteminfo.Name +
                        (selectItem.Iteminfo.IsEquip ? "[E]" : "");//장착상태 표시해주기
                    Utils.CursorMenu.Add((displayName, () =>
                    {
                        Console.Clear();
                        UseItem(Inventory.FindIndex(x=>x.Equals(selectItem)));
                    }
                    ));
                }
                Utils.DisplayLine(true, 2);
                //소비 아이템 출력할곳
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
                    int count = Inventory.Count(item => item is IConsumable&&item.Iteminfo.Name == itemName);//중복개수 세기
                    string displayName = selectItem.Iteminfo.Name +
                        (count!=0 ? $" -({count})" : "이게 보인다면 버그입니다.");//아이템 갯수 표시
                    Utils.CursorMenu.Add((displayName, () =>
                    {
                        UseItem(Inventory.FindIndex(x => x.Equals(selectItem)));
                    }
                    ));
                }
                Utils.CursorMenu.Add(("나가기", () => { Program.CurrentScene = new Scene_Lobby(); }));
                Utils.DisplayCursorMenu(5, 7,300);
            }
        }
        public void UseItem(int j)
        {
            var selectItem = Inventory[j];//캡쳐 방지
            IEquipable? equipable = selectItem as IEquipable;
            IConsumable? consumable = selectItem as IConsumable;

            if (consumable != null && consumable.Consume())//소비하는 아이템이라면
            {
                Inventory.Remove(selectItem);//인벤토리 리스트에서 삭제
            }
            else if (equipable != null)//장비하는 아이템이라면
            {
                equipable.Equip();//장비 메서드호출
            }
            else
            {
                Utils.WriteColorLine("착용불가한 아이템", ConsoleColor.Red);
                Console.ReadKey(); // 대기
            }
        }
    }
}