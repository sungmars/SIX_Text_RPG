namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Lobby : Scene_Base
    {
        private readonly int LEFT = 23;
        private readonly int TOP = 1;
        private readonly string[] NUMBERS =
        {
            " .----------------. " ,
            "| .--------------. |" ,
            "| |     ____     | |" ,
            "| |   .'    '.   | |" ,
            "| |  |  .--.  |  | |" ,
            "| |  | |    | |  | |" ,
            "| |  |  `--'  |  | |" ,
            "| |   '.____.'   | |" ,
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |     __       | |" ,
            "| |    /  |      | |" ,
            "| |    `| |      | |" ,
            "| |     | |      | |" ,
            "| |    _| |_     | |" ,
            "| |   |_____|    | |" ,
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |    _____     | |" ,
            "| |   / ___ `.   | |" ,
            "| |  |_/___) |   | |" ,
            "| |   .'____.'   | |" ,
            "| |  / /____     | |" ,
            "| |  |_______|   | |" ,
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |    ______    | |" ,
            "| |   / ____ `.  | |" ,
            "| |   `'  __) |  | |" ,
            "| |   _  |__ '.  | |" ,
            "| |  | \\____) |  | |",
            "| |   \\______.'  | |",
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |   _    _     | |" ,
            "| |  | |  | |    | |" ,
            "| |  | |__| |_   | |" ,
            "| |  |____   _|  | |" ,
            "| |      _| |_   | |" ,
            "| |     |_____|  | |" ,
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |   _______    | |" ,
            "| |  |  _____|   | |" ,
            "| |  | |____     | |" ,
            "| |  '_.____''.  | |" ,
            "| |  | \\____) |  | |",
            "| |   \\______.'  | |",
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |    ______    | |" ,
            "| |  .' ____ \\   | |",
            "| |  | |____\\_|  | |",
            "| |  | '____`'.  | |" ,
            "| |  | (____) |  | |" ,
            "| |  '.______.'  | |" ,
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |   _______    | |" ,
            "| |  |  ___  |   | |" ,
            "| |  |_/  / /    | |" ,
            "| |      / /     | |" ,
            "| |     / /      | |" ,
            "| |    /_/       | |" ,
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |     ____     | |" ,
            "| |   .' __ '.   | |" ,
            "| |   | (__) |   | |" ,
            "| |   .`____'.   | |" ,
            "| |  | (____) |  | |" ,
            "| |  `.______.'  | |" ,
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |    ______    | |" ,
            "| |  .' ____ '.  | |" ,
            "| |  | (____) |  | |" ,
            "| |  '_.____. |  | |" ,
            "| |  | \\____| |  | |",
            "| |   \\______,'  | |",
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n",

            " .----------------. " ,
            "| .--------------. |" ,
            "| |              | |" ,
            "| |      _       | |" ,
            "| |     (_)      | |" ,
            "| |      _       | |" ,
            "| |     (_)      | |" ,
            "| |              | |" ,
            "| |              | |" ,
            "| '--------------' |" ,
            " '----------------'\n"
        };
        private readonly string[] NOTICE =
        {
            " 내일배움 캠프에 오신 여러분 환영합니다. 출석은 누르셨나요?",
            " 여러분들 C# 체크리스트 특강 들어오세요!",
            " 여러분 점심은 맛있게 드셨나요?",
            " 벌써 저녁 시간이네요. 여러분들 식사 맛있게 드세요.",
            " 퇴근 시간에 찾아오면 범죄인거 아시죠?"
        };

        private int currentTime;

        public override void Awake()
        {
            base.Awake();

            Menu.Add("상태 보기");
            Menu.Add("가방 보기");
            Menu.Add("매니저님 찾아가기 (상점)");
            Menu.Add("튜터님 찾아가기 (전투)");
            Menu.Add("캠 끄기 (휴식)");
            zeroText = "게임 종료";
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    GameManager.Instance.CurrentStage = 3;
                    Display_ClockAnim(2059);
                    //Program.CurrentScene = new Scene_LevelUp();
                    break;
                case 2:
                    // TODO: 팀 스크럼때, 주석 해제
                    Utils.WriteAnim($"튜터님께 걸어가는 중...");
                    Utils.WriteColor(" >> ", ConsoleColor.DarkYellow);
                    Utils.WriteAnim("탈것이 없어 시간이 지체되는 중...");
                    Utils.WriteColor(" >> ", ConsoleColor.DarkYellow);
                    Utils.WriteAnim("뚜벅. 뚜벅. 뚜벅. 뚜벅.");
                    Program.CurrentScene = new Scene_BattleStart();
                    break;
                case 3:
                    Program.CurrentScene = new Scene_Store();
                    break;
                case 0:
                    Utils.Quit();
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Console.SetCursorPosition(1, 1);
            Utils.WriteColorLine(Define.GAME_TITLE, ConsoleColor.DarkYellow);

            Console.SetCursorPosition(1, 3);
            int currentStage = GameManager.Instance.CurrentStage;
            Utils.WriteColorLine("현재 시간: ", currentStage == 4 ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen);

            currentTime = Define.TIMES[currentStage];
            Display_Clock(currentStage == 4 ? ConsoleColor.Red : ConsoleColor.Green);

            Console.ForegroundColor = currentStage == 4 ? ConsoleColor.DarkRed : ConsoleColor.DarkCyan;
            Utils.WriteAnim(NOTICE[currentStage], ConsoleColor.DarkCyan);
            Console.ResetColor();
        }

        private void Display_Clock(ConsoleColor color)
        {
            int time = currentTime;
            for (int i = 4; i >= 0; i--)
            {
                if (i == 2)
                {
                    Display_Number(i, 10, color);
                    continue;
                }

                Display_Number(i, time % 10, color);
                time /= 10;
            }
        }

        private void Display_ClockAnim(int targetTime)
        {
            int minute = 0;
            while (targetTime > currentTime)
            {
                currentTime++;
                Thread.Sleep(6);

                minute++;
                if (minute == 60)
                {
                    currentTime += 40;
                    minute = 0;
                }

                Display_Clock(ConsoleColor.Green);
            }

            GameManager.Instance.CurrentStage++;
            Program.CurrentScene = new Scene_Lobby();
        }

        private void Display_Number(int index, int number, ConsoleColor color)
        {
            Console.SetCursorPosition(0, TOP);

            int numberIndex = number * 11;
            for (int i = 0; i < 11; i++)
            {
                Console.SetCursorPosition(LEFT + index * 19, Console.CursorTop);
                Utils.WriteColorLine(NUMBERS[numberIndex + i], color);
            }
        }
    }
}