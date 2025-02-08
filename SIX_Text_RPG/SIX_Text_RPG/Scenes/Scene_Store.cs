using SIX_Text_RPG;
using SIX_Text_RPG.Scenes;
using System;
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

        private static readonly string[] talksell =
        {
            "안녕하세요 name님. 무슨 일 있으신가요?",
            "흠... 아이템을 환불해달라고요?",
            "지금 저한테 buy하신거 맘에 안 드시나요?",
            "이번만 입니다... But 포인트의 80%만 돌려드릴거에요."
        };

        private static readonly string[] talkgambling =
        {
            "안녕하세요 name님. 무슨 일 있으신가요?",
            "흠... 도박을 하러 왔다고요..? 저희 그런 클럽 아닙니다.",
            "다 알고 오셨다니, 어쩔 수 없네요... 이건 다른 매니저님들에게는 secret입니다."
        };

        private string[,] item =
        {
            {"" }
        }



        //아이템사기 델리게이트
        private Action<int, int> buying;

        public override void Awake()
        {
            Menu.Add("아이템 사기");
            Menu.Add("아이템 팔기");
            Menu.Add("도?박");

            sceneTitle = "수상한 매니저님 방";
            sceneInfo = "수상한..? 자세히보니 수상한이 아니라 송승환 매니저님이라 적혀있습니다...";

            
        }

        public override int Update()
        {
            switch (base.Update())
            {
                //아이템사기 구현
                case 1:
                    Talking(talkBuy);
                    break;

                //아이템팔기 구현
                case 2:
                    Talking(talksell);
                    break;

                //도박
                case 3:
                    Talking(talkgambling);
                    break;

                //나가기
                case 0:
                    Program.CurrentScene = new Scene_Lobby();
                    break;
            }
            return 0;
        }

        protected override void Display()
        {
            
        }

        private void Talking(string[] talk)
        {
            for (int i = 0; i < talk.Length; i++)
            {
                Utils.WriteColor("송승환 매니저님", ConsoleColor.DarkCyan);
                Utils.WriteName(talk[i]);

                Console.ReadLine();
                Console.WriteLine();
            }
        }

        private void Buy()
        {
            
        }
    }
}
