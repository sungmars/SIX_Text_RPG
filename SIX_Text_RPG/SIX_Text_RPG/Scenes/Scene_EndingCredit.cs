using System;
using System.Collections.Generic;
using System.Threading;
using SIX_Text_RPG.Managers;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_EndingCredit : Scene_Base
    {
        private readonly List<string> credits = new()
        {
            "송석호(조장): 모든 씬 코드 총괄 및 토대 제작",
            "",
            "최재혁: 퀘스트 씬 제작",
            "",
            "박한나: 상점, 아이템 클래스 구조체 제작",
            "",
            "이성재: 인벤토리 제작",
            "",
            "김동현: 배틀로비와 전투씬 제작",
            "",
            "감사합니다!",
            "",
            "The End"
        };

        private int height = Console.WindowHeight;
        private int xPos = 40; 
        private int step = 0; 
        private int max; 
        private int startPos; 
        private int displayCount = 0;

        public override void Awake()
        {
            base.Awake();
            sceneTitle = "엔딩 크레딧";
            max = height; 
            startPos = 0; // 첫 번째 문장이 시작하는 위치
        }

        protected override void Display()
        {
            Console.Clear();
            Utils.ClearBuffer();
        }

        public override int Update()
        {
            Console.Clear(); 

            int startLine = Math.Max(0, step - max + 1); // 현재 표시해야 할 줄 계산

           
            if (displayCount < credits.Count)
            {
                displayCount++;
            }

            for (int i = 0; i < displayCount; i++) // 출력할 줄 개수만큼 표시
            {
                int position = startPos + i; 

                if (position < height) 
                {
                    Console.SetCursorPosition(xPos, position);
                    Utils.WriteColorLine(credits[credits.Count - displayCount + i], ConsoleColor.DarkCyan);
                }
            }

       
            if (displayCount == credits.Count)
            {
                startPos++; // 한 칸씩 이동하면서 아래부터 지움
            }

            if (startPos > credits.Count + max) 
            {
                Console.Clear();
                Console.SetCursorPosition(xPos, height / 2);
                Utils.WriteColorLine("\n 게임이 종료되었습니다. 엔터를 눌러주세요.", ConsoleColor.DarkGray);
                Console.ReadKey();
                Utils.Quit();
                return 0;
            }

            step++;
            Thread.Sleep(500);
            return -1; // 계속 실행
        }
    }
}
