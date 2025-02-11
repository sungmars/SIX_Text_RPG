﻿using SIX_Text_RPG;
using SIX_Text_RPG.Scenes;
using System;
using System.Runtime.ConstrainedExecution;
namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Store : Scene_Base
    {
        private static readonly string[] talkBuy =
        {
            "안녕하세요 name님. 무슨 일 있으신가요?",
            "흠... 아이템을 사러왔다고요?",
            "하.. 이거 참. 아무한테나 보여주는게 아닌데",
            "이거 다른 매니저님들에게는 secret입니다."
        };

        private static readonly string[] talkSell =
        {
            "안녕하세요 name님. 무슨 일 있으신가요?",
            "흠... 아이템을 환불해달라고요?",
            "지금 저한테 buy하신거 맘에 안 드시나요?",
            "이번만 입니다... But 포인트의 80%만 return 해드릴거에요."
        };

        private static readonly string[] talkGambling =
        {
            "안녕하세요 name님. 무슨 일 있으신가요?",
            "흠... 도박을 하러 왔다고요..? 저희 그런 클럽 아닙니다.",
            "다 알고 오셨다니, 어쩔 수 없네요... 이건 다른 매니저님들에게는 secret입니다."
        };

        private static readonly string[] talkFirst =
        {
            "오 name 님 안녕하세요~",
            "여기는 어쩐 일이실까요?"
        };

        private int left;
        private int top;

        private bool isTalk_1 = false;
        private bool isTalk_2 = false;
        private bool isTalk_3 = false;


        public override void Awake()
        {
            Console.Clear();
            //메뉴추가 
            Menu.Add("\"아이템 사기\"");
            Menu.Add("아이템 팔기");
            Menu.Add("도?박");

            //씬 타이틀 인포
            sceneTitle = "수상한 매니저님 방";
            sceneInfo = "수상한..? 아! 자세히보니 수상한이 아니라 송승환 매니저님이라 적혀있습니다...";
        }

        public override int Update()
        {
            switch (base.Update())
            {
                //아이템 구매
                case 1:
                    if (!isTalk_1)
                    {
                        Talking(talkBuy);
                        isTalk_1 = true;
                    }
                    Program.CurrentScene = new Scene_Store_Buy();
                    break;

                //아이템 판매
                case 2:
                    Program.CurrentScene = new Scene_Store_Sell();
                    break;

                //골드 도박
                case 3:
                    //Program.CurrentScene = new Scene_Store_Gambling();
                    break;

                case 0:
                    Program.CurrentScene = new Scene_Lobby();
                    break;
                 //테스트용 코드
                case 9:
                    GameManager.Instance.Player.SetStat(Stat.Gold, 10000);
                    break;
            }
            return 0;
        }

        protected override void Display()
        {
            Talking(talkFirst);
        }

        private void Talking(string[] talk)
        {
            Console.WriteLine();
            Console.SetCursorPosition(1, 7);
            Utils.WriteColor("송승환 매니저님\n", ConsoleColor.DarkCyan);
            for (int i = 0; i < talk.Length; i++)
            {
                Utils.ClearLine(0, 8);
                Utils.ClearLine(0, 9);
                Console.SetCursorPosition(1, 8);
                Utils.WriteAnim(talk[i]);
                Console.SetCursorPosition(1, 9);
                Utils.WriteColor(">> ", ConsoleColor.DarkYellow);
                Console.ReadLine();
            }
        }
    }
}

