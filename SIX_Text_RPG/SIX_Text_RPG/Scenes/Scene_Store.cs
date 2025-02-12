namespace SIX_Text_RPG.Scenes
{
    public enum ScriptType
    {
        Store,
        StoreBuy,
        StoreSell,
        StoreGamble,
        Count
    }

    internal class Scene_Store : Scene_Base
    {
        private static readonly string[][] scripts =
        {
            [
                "오 name님 안녕하세요~",
                "여기는 어쩐 일이실까요?"
            ],
            [
                "안녕하세요 name님. 무슨 일 있으신가요?",
                "흠... 아이템을 사러왔다고요?",
                "하.. 이거 참. 아무한테나 보여주는게 아닌데",
                "이거 다른 매니저님들에게는 secret입니다."
            ],
            [
                "안녕하세요 name님. 무슨 일 있으신가요?",
                "흠... 아이템을 환불해달라고요?",
                "지금 저한테 buy하신거 맘에 안 드시나요?",
                "이번만 입니다... But 포인트의 80%만 return 해드릴거에요."
            ],
            [
                "안녕하세요 name님. 무슨 일 있으신가요?",
                "흠... 도박을 하러 왔다고요..? 저희 그런 클럽 아닙니다.",
                "다 알고 오셨다니, 어쩔 수 없네요... 이건 다른 매니저님들에게는 secret입니다."
            ]
        };

        public static bool[] ScriptToggles { get; set; } = new bool[(int)ScriptType.Count];

        public override void Awake()
        {
            base.Awake();

            // 메뉴 추가
            Menu.Add("아이템 \'사기\'");
            Menu.Add("아이템 팔기");
            Menu.Add("도?박");

            //씬 타이틀 인포
            sceneTitle = "수상한 매니저님 방";
            sceneInfo = "수상한..? 아! 자세히보니 수상한이 아니라 송승환 매니저님이라 적혀있습니다...";

            AudioManager.Instance.Play(AudioClip.Music_Shop);
        }

        public override int Update()
        {
            switch (base.Update())
            {
                //아이템 구매
                case 1:
                    Program.CurrentScene = new Scene_StoreBuy();
                    break;

                //아이템 판매
                case 2:
                    Program.CurrentScene = new Scene_Store_Sell();
                    break;

                //골드 도박
                case 3:
                    Program.CurrentScene = new Scene_StoreGambling();
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
            Scripting(ScriptType.Store);
        }

        public static void Scripting(ScriptType scriptType)
        {
            if (ScriptToggles[(int)scriptType])
            {
                return;
            }

            if (scriptType > ScriptType.Store)
            {
                ScriptToggles[(int)scriptType] = true;
            }

            Console.SetCursorPosition(1, 7);
            Utils.WriteColor("송승환 매니저님\n", ConsoleColor.DarkCyan);

            var script = scripts[(int)scriptType];
            for (int i = 0; i < script.Length; i++)
            {
                if (i > 0)
                {
                    Console.ReadKey(true);
                    Utils.ClearLine(0, 8);
                }

                Console.SetCursorPosition(1, 8);
                Utils.WriteAnim(script[i]);
            }

            Utils.ClearBuffer();
        }

        public static void ClearContent()
        {
            for (int i = 7; i < Console.WindowHeight; i++)
            {
                Utils.ClearLine(0, i);
            }

            Console.SetCursorPosition(0, 0);
            Utils.CursorMenu.Clear();
        }
    }
}