namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleLobby : Scene_Base
    {
        private readonly int LEFT = 99;
        private readonly int TOP = 16;
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

        private readonly Random random = new Random();
        private readonly List<Monster> monsters = new List<Monster>();

        public override void Awake()
        {
            base.Awake();

            hasZero = false;
            sceneTitle = "튜터 ZONE";

            // 공격 메뉴 추가
            Menu.Add("공격");
            // 아이템 메뉴 추가
            MakeMonster();
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    // TEST CODE
                    Console.SetCursorPosition(0, 25);
                    GameManager.Instance.DisplayBattle(0, 6, () =>
                    {
                        GameManager.Instance.Monsters[0].Damaged(5);
                    });
                    //Program.CurrentScene = new Scene_PlayerAttack(monsters);
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            // 플레이어 정보 출력
            player.DisplayInfo_Status();

            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
            Utils.DisplayLine();

            (int left, int top) = Console.GetCursorPosition();

            // VS 아스키 텍스트 출력
            for (int i = 0; i < VERSUS.Length; i++)
            {
                Console.SetCursorPosition(50, TOP - 2 + i);
                Utils.WriteColorLine(VERSUS[i], i > 2 ? ConsoleColor.DarkRed : ConsoleColor.Red);
            }

            // 모든 몬스터 정보 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                Console.SetCursorPosition(LEFT, TOP + i);
                monsters[i].DisplayMonster();
            }

            Console.SetCursorPosition(left, top);
        }

        // 랜덤 1-4명의 몬스터 생성
        private void MakeMonster()
        {
            if (monsters.Count > 0)
            {
                return;
            }

            int monsterCount = random.Next(1, 5);
            for (int i = 0; i < monsterCount; i++)
            {
                monsters.Add(SetMonster(random.Next(1, 4)));
            }
        }

        // 3가지 종유릐 입력받은 값의 몬스터 설정
        private Monster SetMonster(int index)
        {
            return new Monster((MonsterType)index);
        }
    }
}