using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Inventory:Scene_Base
    {
        private readonly List<Item> Inventory = new List<Item>();
        private readonly int LEFT = 0;
        private int cursorIndex;
        private int cursorTop;
        
        public override void Awake()
        {
            base.Awake();
            sceneTitle = "아이템 창";
            sceneInfo = "";
            hasZero = false;
            Display();
        }

        public override int Update()
        {
            int cursor = LEFT - 3;
            while (Program.CurrentScene == this)
            {
                var key =Console.ReadKey(true);
                if (key.Key==ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(cursor,cursorTop+cursorIndex);
                    Console.Write(' ');

                    cursorIndex = Math.Max(cursorIndex - 1, 0);
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Utils.WriteColor("▶", ConsoleColor.DarkCyan);
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Console.Write(' ');

                    cursorIndex = Math.Min(cursorIndex + 1, Inventory.Count - 1);
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Utils.WriteColor("▶", ConsoleColor.DarkCyan);
                }
                if (key.Key == ConsoleKey.Enter)
                {
                    ItemSelect(Inventory[cursorIndex]);

                }
            }
            return 0;
        }
        protected override void Display()
        {
            Console.SetCursorPosition(0, 0);
            //아이템 리스트 출력
            cursorTop = Console.CursorTop + 1;
            for(int i=0;i<Inventory.Count;i++)
            {
                Console.SetCursorPosition(LEFT, cursorTop+i);
                if (i == cursorIndex)
                {
                    Utils.WriteColor("▶ ", ConsoleColor.DarkCyan);
                }
                else
                {
                    Console.Write("   ");
                }
                Utils.WriteColorLine(Inventory[i].Name,ConsoleColor.White);
            }
        }
        private void ItemSelect(Item selctedItem)
        {
            Utils.WriteColorLine($"\n {아이템 프로퍼티.Name}가 선택되었습니다.",ConsoleColor.DarkYellow);
        }

    }
}
