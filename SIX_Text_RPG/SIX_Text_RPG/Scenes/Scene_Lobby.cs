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
            " 여러분들 C# 체크리스트 특강 들어오셔야 합니다!",
            " 여러분 점심은 맛있게 드셨나요? 저도 맛있게 먹었습니다.",
            " 벌써 저녁 시간이네요? 여러분들 식사 맛있게 드시고 오세요!",
            " 퇴근 시간에 찾아오면 범죄인거 아시죠?"
        };

        public int currentTime;

        public override void Awake()
        {
            base.Awake();

            Menu.Add("상태 보기");
            Menu.Add("가방 보기");
            Menu.Add("매니저님 찾아가기 (상점)");
            Menu.Add("튜터님 찾아가기 (전투)");
            Menu.Add("일일 스크럼 (퀘스트)");
            Menu.Add("캠 끄기 (휴식)");
            Menu.Add("TIL 작성 (저장)");
            zeroText = "게임 종료";

            AudioManager.Instance.Play(AudioClip.Music_Title);
        }

        public override int Update()
        {
            if (GameManager.Instance.CurrentStage == 4)
            {
                GameManager.Instance.CurrentStage++;
                return 0;
            }

            switch (base.Update())
            {
                case 1: // 상태 보기
                    Program.CurrentScene = new Scene_PlayerInfo();
                    break;
                case 2: // 가방 보기
                    Program.CurrentScene = new Scene_Inventory();
                    break;
                case 3: // 상점
                    Program.CurrentScene = new Scene_Store();
                    break;
                case 4: // 전투
                    Utils.WriteAnim($"튜터님께 걸어가는 중...");
                    Utils.WriteColor(" >> ", ConsoleColor.DarkYellow);
                    Utils.WriteAnim("탈것이 없어 시간이 지체되는 중...");
                    Utils.WriteColor(" >> ", ConsoleColor.DarkYellow);
                    Utils.WriteAnim("뚜벅. 뚜벅. 뚜벅. 뚜벅.");
                    if (GameManager.Instance.Player != null)
                    {
                        GameManager.Instance.BattleBeforeHP = GameManager.Instance.Player.Stats.HP;
                        GameManager.Instance.BattleBeforeMP = GameManager.Instance.Player.Stats.MP;
                    }
                    if (GameManager.Instance.TargetStage == 4)
                    {
                        Program.CurrentScene = new Scene_LastStage();
                    }
                    else
                    {
                        Program.CurrentScene = new Scene_BattleLobby();
                    }
                    break;
                case 5: // 퀘스트
                    Program.CurrentScene = new Scene_QuestTable();
                    break;
                case 6: // 휴식
                    Program.CurrentScene = new Scene_Rest();
                    break;
                case 7: // 저장
                    AudioManager.Instance.Play(AudioClip.SoundFX_Confirm);
                    DataManager.Instance.SaveData();
                    Console.WriteLine("저장이 완료되었습니다.");
                    Console.ReadKey();
                    break;
                case 0:
                    if (GameManager.Instance.CurrentStage == 5)
                    {
                        Program.CurrentScene = new Scene_EndingCredit();
                        return 0;
                    }
                    Utils.Quit();
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Console.SetCursorPosition(1, 1);
            Utils.WriteColorLine(Define.GAME_TITLE, ConsoleColor.DarkYellow);

            GameManager.Instance.CurrentStage = Math.Max(GameManager.Instance.CurrentStage, 0);
            int index = Math.Min(GameManager.Instance.CurrentStage, 4);
            currentTime = Define.TIMES[index];
            int targetStage = GameManager.Instance.TargetStage;

            Console.SetCursorPosition(1, 3);
            Utils.WriteColorLine("현재 시간: ", targetStage == 4 ? ConsoleColor.DarkRed : ConsoleColor.DarkGreen);
            Display_Clock(targetStage == 4 ? ConsoleColor.Red : ConsoleColor.Green);

            if (targetStage <= GameManager.Instance.CurrentStage)
            {
                Utils.WriteColorLine(NOTICE[targetStage], targetStage == 4 ? ConsoleColor.DarkRed : ConsoleColor.DarkCyan);
                return;
            }

            Display_ClockAnim(Define.TIMES[targetStage]);
            Utils.WriteAnim(NOTICE[targetStage], targetStage == 4 ? ConsoleColor.DarkRed : ConsoleColor.DarkCyan);
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
            (int left, int top) = Console.GetCursorPosition();
            AudioManager.Instance.Play(AudioClip.SoundFX_ClockTicking);

            int minute = 0;
            while (targetTime > currentTime)
            {
                currentTime++;
                Thread.Sleep(2);

                minute++;
                if (minute == 60)
                {
                    currentTime += 40;
                    minute = 0;
                    AudioManager.Instance.Play(AudioClip.SoundFX_ClockTicking);
                }

                Display_Clock(ConsoleColor.Green);
            }

            AudioManager.Instance.Stop(AudioClip.SoundFX_ClockTicking);
            GameManager.Instance.CurrentStage++;
            Console.SetCursorPosition(left, top);
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

        public override void LateStart()
        {
            if (GameManager.Instance.CurrentStage == 4)
            {
                return;
            }

            base.LateStart();
        }
    }
}