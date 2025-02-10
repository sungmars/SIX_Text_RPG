using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Inventory : Scene_Base
    {
        private readonly List<Item> Inventory = new List<Item>();//이거 게임매니저로 갖고오게 해야함
        
        public override void Awake()
        {
            base.Awake();
            sceneTitle = "인벤토리";
            sceneInfo = "";
            hasZero = false;
            MakeItem();
            
        }

        
        protected override void Display()
        {
            //Console.SetCursorPosition(0, 0);
            ////아이템 리스트 출력
            //cursorTop = Console.CursorTop + 1;
            //int totalmenu = Inventory.Count + 1;
            //for (int i = 0; i < totalmenu; i++)//Count+1은 나가기 버튼
            //{
            //    Console.SetCursorPosition(LEFT, cursorTop + i);
            //    if (i == cursorIndex)
            //    {
            //        Utils.WriteColor("▶", ConsoleColor.DarkCyan);
            //    }
            //    else
            //    {
            //        Console.Write("");
            //    }
            //    if (i < Inventory.Count)
            //    {
            //        Utils.WriteColorLine(Inventory[i].info.Name, ConsoleColor.White);
            //    }
            //    else
            //    {
            //        Utils.WriteColorLine("  나가기", ConsoleColor.Gray);
            //    }
            //}
            //Utils.CursorMenu.Clear();
            if (Inventory.Count == 0)//아이템이 없다면 아무것도 없다는 문장과 엔터키를 누르면 나가는 기능
            {
                Utils.WriteColorLine("현재 가방에 있는 것: 공기", ConsoleColor.DarkGray);
                Utils.WriteColorLine("엔터 키 누르면 이전 메뉴로 돌아갑니다.", ConsoleColor.DarkGray);
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Program.CurrentScene = new Scene_Lobby();
                    
                    return;
                }
            }
            
            // 인벤토리의 커서메뉴 추가
            for (int i = 0; i < Inventory.Count; i++)
            {
                string displayName = (Inventory[i].info.IsEquipped ? "[E]" : "")+Inventory[i].info.Name ;//상태에 따라 표시할 이름 생성
                var selectItem = Inventory[i];//캡쳐 방지
                
                Utils.CursorMenu.Add((
                    displayName, // 메뉴에 출력될 아이템 이름
                    () =>
                    {
                        Console.Clear();
                        //장착 상태에 따라 토글
                        if (selectItem.info.IsEquipped==false)
                        {
                            selectItem.info.IsEquipped = true;
                        }
                        else
                        {
                            selectItem.info.IsEquipped = false;
                        }
                    }
                ));
                
                
            }
            Utils.CursorMenu.Add((
                    "나가기",
                    () =>
                    {
                        Program.CurrentScene = new Scene_Lobby();
                    }
            ));
            Utils.DisplayCursorMenu(5, 5);
            
        }
        private void ItemSelect(Item selectedItem)//아이템 장착 메서드
        {
            Utils.WriteColorLine($"\n {selectedItem.info.Name}가 선택되었습니다.", ConsoleColor.DarkYellow);
        }
        
        // 아래는 출력 시험하기 위해 추가한 코드
        private void MakeItem()
        {
            Inventory.Add(SetItem("찌르기 클로"));
            Inventory.Add(SetItem("롱기누스의 손가락"));
            Inventory.Add(SetItem("강철 손가락"));
        }

        private Item SetItem(string name)
        {

            Item item = new Item() { info = new() { Name = name, Description = "", IsSold = true, Price = 100 } };
            return item;
        }

        private void ResetCursorPosition(int left,int top)
        {
            Console.SetCursorPosition(left, top+1);
        }
    
    }
        
}


