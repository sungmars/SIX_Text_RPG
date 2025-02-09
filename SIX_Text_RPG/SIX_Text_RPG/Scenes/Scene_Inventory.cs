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
        private readonly List<Item> Inventory = new List<Item>();
        private readonly int LEFT = 0;
        private int cursorIndex;
        private int cursorTop;

        public override void Awake()
        {
            base.Awake();
            sceneTitle = "인벤토리";
            sceneInfo = "";
            hasZero = false;
            MakeItem();
            Display();
        }

        public override int Update()
        {
            int totalmenu = Inventory.Count + 1;
            while (Program.CurrentScene == this)
            {
                int cursor = LEFT;
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Console.Write(' ');

                    cursorIndex = Math.Max(cursorIndex - 1, 0);
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Utils.WriteColor("▶", ConsoleColor.DarkCyan);
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Console.Write(' ');
                    
                    cursorIndex = Math.Min(cursorIndex + 1, totalmenu - 1);
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Utils.WriteColor("▶", ConsoleColor.DarkCyan);
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    if (cursorIndex < Inventory.Count)//나가기 버튼 기능 추가
                    {
                        ItemSelect(Inventory[cursorIndex]);
                    }
                    else
                    {
                        Program.CurrentScene = new Scene_Lobby();
                    }
                }
            }
            return 0;
        }
        protected override void Display()
        {
            Console.SetCursorPosition(0, 0);
            //아이템 리스트 출력
            cursorTop = Console.CursorTop + 1;
            int totalmenu = Inventory.Count + 1;
            for (int i = 0; i < totalmenu; i++)//Count+1은 나가기 버튼
            {
                Console.SetCursorPosition(LEFT, cursorTop + i);
                if (i == cursorIndex)
                {
                    Utils.WriteColor("▶", ConsoleColor.DarkCyan);
                }
                else
                {
                    Console.Write("");
                }
                if (i < Inventory.Count)
                {
                    Utils.WriteColorLine(Inventory[i].info.Name, ConsoleColor.White);
                }
                else
                {
                    Utils.WriteColorLine("  나가기", ConsoleColor.Gray);
                }
            }
        }
        private void ItemSelect(Item selectedItem)//아이템 장착 메서드
        {
            Utils.WriteColorLine($"\n {selectedItem.info.Name}가 선택되었습니다.", ConsoleColor.DarkYellow);
        }
        
        // 아래는 출력 시험하기 위해 추가한 코드
        private void MakeItem()
        {
            Inventory.Add(SetItem("찌르기 클로"));
            Inventory.Add(SetItem("  롱기누스의 손가락"));
            Inventory.Add(SetItem("  강철 손가락"));
        }

        private Item SetItem(string name)
        {

            Item item = new Item() { info = new() { Name = name, Description = "", IsSold = true, Price = 100 } };
            return item;
        }
    
    }
        
}


