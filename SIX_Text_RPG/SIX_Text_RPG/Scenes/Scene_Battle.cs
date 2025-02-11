namespace SIX_Text_RPG.Scenes
{
    internal abstract class Scene_Battle : Scene_Base
    {
        protected readonly int CURSORMENU_TOP = 22;

        private readonly int MONSTER_LEFT = 99;
        private readonly int VERSUS_TOP = 5;
        private readonly string[] VERSUS_TEXT =
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

        protected static bool HasAnim => Program.PreviousScene is not Scene_Battle;

        protected readonly Random random = new();
        protected readonly Player? player = GameManager.Instance.Player;
        protected readonly List<Monster> monsters = GameManager.Instance.Monsters;

        protected int monsterIndex;

        public override void Awake()
        {
            hasZero = false;
            Utils.WriteColorLine("\n 질문 VS 피드백\n\n", ConsoleColor.DarkYellow);
        }

        public override void LateStart()
        {
            base.LateStart();
            Program.PreviousScene = Program.CurrentScene;
        }

        protected override void Display()
        {
            if (player == null)
            {
                return;
            }

            if (HasAnim)
            {
                AudioManager.Instance.Play(AudioClip.Music_Battle);
                Thread.Sleep(500);
            }

            // 플레이어 정보 출력
            Console.SetCursorPosition(0, 6);
            player.DisplayInfo_Status();

            if (HasAnim) Thread.Sleep(500);

            // 현재 커서위치 저장
            (int left, int top) = Console.GetCursorPosition();

            // VS 아스키 텍스트 출력
            for (int i = 0; i < VERSUS_TEXT.Length; i++)
            {
                Console.SetCursorPosition(50, VERSUS_TOP + i);
                Utils.WriteColorLine(VERSUS_TEXT[i], i > 2 ? ConsoleColor.DarkRed : ConsoleColor.Red);
            }

            // 모든 몬스터 정보 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                if (HasAnim)
                {
                    Thread.Sleep(500);
                }

                Console.SetCursorPosition(MONSTER_LEFT, VERSUS_TOP + 6 - i);
                monsters[monsters.Count - i - 1].DisplayMonster();
            }

            if (HasAnim) Thread.Sleep(500);

            // 커서 위치 초기화 및 이중선 그리기
            Console.SetCursorPosition(left, top);
            Utils.DisplayLine(HasAnim, 3);
        }
    }
}