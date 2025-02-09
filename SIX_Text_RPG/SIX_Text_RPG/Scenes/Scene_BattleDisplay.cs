using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleDisplay : Scene_Base
    {
        private readonly int LEFT = 92;
        private readonly int TOP = 5;
        private readonly string[] VERSUS =
        {
            " /$$    /$$  /$$$$$$   ",
            "| $$   | $$ /$$__  $$  ",
            "| $$   | $$| $$  \\__/ ",
            "|  $$ / $$/|  $$$$$$   ",
            " \\  $$ $$/  \\____  $$",
            "  \\  $$$/   /$$  \\ $$",
            "   \\  $/   |  $$$$$$/ ",
            "    \\_/     \\______/",
        };

        private Player? player = GameManager.Instance.Player;
        private List<Monster> monsters = GameManager.Instance.Monsters;
        protected bool isSelectMonster = false;

        public override void Awake()
        {
            base.Awake();

            hasZero = false;
            sceneTitle = "튜터 ZONE";
            //sceneInfo =
            //    "\n                 ####    ########        ###       ########    ########    ##          ########    ####  " +
            //    "\n                 ####    ##     ##      ## ##         ##          ##       ##          ##          ####  " +
            //    "\n                 ####    ##     ##     ##   ##        ##          ##       ##          ##          ####  " +
            //    "\n                  ##     ########     ##     ##       ##          ##       ##          ######       ##   " +
            //    "\n                         ##     ##    #########       ##          ##       ##          ##                " +
            //    "\n                 ####    ##     ##    ##     ##       ##          ##       ##          ##          ####  " +
            //    "\n                 ####    ########     ##     ##       ##          ##       ########    ########    ####\n";

        }

        public override int Update()
        {
            return base.Update();
        }

        protected override void Display()
        {
            if (player == null)
            {
                return;
            }


            // 플레이어 정보 출력
            player.DisplayInfo_Status();

            // 커서위치 저장
            (int left, int top) = Console.GetCursorPosition();


            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Utils.DisplayLine();

            // VS 아스키 텍스트 출력
            for (int i = 0; i < VERSUS.Length; i++)
            {
                Console.SetCursorPosition(50, TOP - 1 + i);
                Utils.WriteColorLine(VERSUS[i], i > 2 ? ConsoleColor.DarkRed : ConsoleColor.Red);
            }

            // 모든 몬스터 정보 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.SetCursorPosition(LEFT, TOP + i);
                if(isSelectMonster)
                {
                    if (monsters[i].IsDead)
                    {
                        Utils.WriteColor($" {i + 1}. ", ConsoleColor.DarkGray);
                    }
                    else
                    {
                        Utils.WriteColor($" {i + 1}. ", ConsoleColor.Red);
                    }
                }
                monsters[i].DisplayMonster();
            }

            // 커서 위치 초기화
            Console.SetCursorPosition(left, top);
        }
    }


}
