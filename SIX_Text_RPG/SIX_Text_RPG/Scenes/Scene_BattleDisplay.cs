namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleDisplay : Scene_Base
    {
        private readonly int LEFT = 99;
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
            "    \\_/     \\______/\n",
        };

        private readonly List<Monster> monsters = GameManager.Instance.Monsters;

        public override void Awake()
        {
            base.Awake();
            hasZero = false;

            Utils.WriteColorLine("\n 질문 VS 피드백\n\n", ConsoleColor.DarkYellow);
        }

        public override void LateStart()
        {
            base.LateStart();
            Utils.DisplayLine(true, 3);
        }

        public override int Update()
        {
            return base.Update();
        }

        protected override void Display()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            // 플레이어 정보 출력
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            player.DisplayInfo_Status();

            // 현재 커서위치 저장
            (int left, int top) = Console.GetCursorPosition();

            // VS 아스키 텍스트 출력
            for (int i = 0; i < VERSUS.Length; i++)
            {
                Console.SetCursorPosition(50, TOP + i);
                Utils.WriteColorLine(VERSUS[i], i > 2 ? ConsoleColor.DarkRed : ConsoleColor.Red);
            }

            Thread.Sleep(500);

            // 모든 몬스터 정보 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                Thread.Sleep(500);

                Console.SetCursorPosition(LEFT, TOP + 6 - i);
                monsters[i].DisplayMonster();
            }

            Thread.Sleep(500);

            // 커서 위치 초기화
            Console.SetCursorPosition(left, top);
        }
    }
}