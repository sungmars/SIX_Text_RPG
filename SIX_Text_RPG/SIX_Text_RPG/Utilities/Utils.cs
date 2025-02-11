using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Utils
    {
        public static List<(string, Action)> CursorMenu { get; private set; } = new();

        private static readonly Random random = new();

        private static int cursorIndex;
        private static int contentLeft;
        private static int contentTop;
        private static int offsetTop;

        // 애니메이션(Thread.Sleep) 재생 중인 동안 눌린 키를 지워줌.
        public static void ClearBuffer()
        {
            Thread.Sleep(100);
            while (Console.KeyAvailable) Console.ReadKey(true);
        }

        // 원하는 콘솔 좌표 시작점 left로부터 top 좌표에 있는 모든 텍스트를 지워준다.
        public static void ClearLine(int left, int top, int right = 0)
        {
            Console.SetCursorPosition(left, top);

            int width = right > 0 ? right : Console.WindowWidth - left;
            Console.Write(new string(' ', width));

            Console.SetCursorPosition(left, top);
        }

        // 커서 메뉴를 출력합니다.
        public static void DisplayCursorMenu(int left, int top, int exitOffsetY = 0, int delay = 0)
        {
            cursorIndex = 0;
            contentLeft = left;
            contentTop = top;
            offsetTop = Math.Abs(exitOffsetY);

            Console.SetCursorPosition(left - 3, top);
            WriteColor("▶", ConsoleColor.DarkCyan);

            Console.SetCursorPosition(left, top);
            for (int i = 0; i < CursorMenu.Count; i++)
            {
                if (i == CursorMenu.Count - 1)
                {
                    int Y = exitOffsetY > 0 ? top + exitOffsetY : Console.CursorTop;
                    Console.SetCursorPosition(left, Y);
                    WriteColorLine(CursorMenu[i].Item1, ConsoleColor.DarkGray);

                    AudioManager.Instance.Play(AudioClip.SoundFX_TaskDone);
                }
                else
                {
                    Console.SetCursorPosition(left, Console.CursorTop);
                    WriteColorLine(CursorMenu[i].Item1, ConsoleColor.Gray);
                }

                Thread.Sleep(delay);
            }
        }

        // 화면에 선을 그립니다. 원할 경우 애니메이션, 이중선도 그릴 수 있습니다.
        public static void DisplayLine(bool hasAnim = false, int secondLineTop = 0)
        {
            int width = Console.WindowWidth;
            int firstX = 0;
            int firstY = Console.CursorTop + 1;
            int secondX = width - 2;
            if (hasAnim)
            {
                AudioManager.Instance.Play(AudioClip.SoundFX_DrawLine);
            }

            while (firstX < width)
            {
                Console.SetCursorPosition(firstX, firstY);
                firstX += 2;
                Console.Write("〓");

                if (secondLineTop > 0)
                {
                    Console.SetCursorPosition(secondX, secondLineTop);
                    secondX -= 2;
                    Console.Write("〓");
                }

                if (hasAnim)
                {
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine();

            ClearBuffer();
        }

        // percent의 확률로 true를 반환합니다.
        public static bool LuckyMethod(int percent)
        {
            return random.Next(1, 101) <= percent;
        }

        // 방향키 입력을 받는 함수 (예외처리 포함)
        public static int ReadArrow()
        {
            var monsters = GameManager.Instance.Monsters;
            if (Program.CurrentScene is Scene_BattleSelect && monsters[0].IsDead)
            {
                ReadArrowKey(ConsoleKey.DownArrow);
            }

            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                var result = ReadArrowKey(input.Key);
                switch (result)
                {
                    case -2:  // 잘못된 값이 입력됬을 경우
                        break;
                    case -1:  // 왼쪽 키가 눌린 경우
                        return -1;
                    case 0:   // 위, 아래 혹은 엔터 키가 눌린 경우
                        return 0;
                    case 1:   // 오른쪽 키가 눌린 경우
                        return 1;
                }
            }
        }

        // 방향키 입력을 재귀하기 위한 함수 (외부 호출 불가)
        private static int ReadArrowKey(ConsoleKey key)
        {
            // 상점 리스트를 좌우로 넘기기 위해 방향값 계산용
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    return -1;
                case ConsoleKey.RightArrow:
                    return 1;
            }

            var monsters = GameManager.Instance.Monsters;
            int cursorLeft = contentLeft - 3;
            if (key == ConsoleKey.UpArrow)
            {
                // 나가기 메뉴(마지막 인덱스) 간격 조절 여부에 따라 커서 위치 설정
                if (cursorIndex == CursorMenu.Count - 1 && offsetTop > 0)
                {
                    Console.SetCursorPosition(cursorLeft, contentTop + offsetTop);
                }
                else
                {
                    Console.SetCursorPosition(cursorLeft, contentTop + cursorIndex);
                }
                Console.Write(' ');

                cursorIndex = Math.Max(cursorIndex - 1, 0);
                if (Program.CurrentScene is Scene_BattleSelect && monsters[cursorIndex].IsDead)
                {
                    // 만약 가장 위라면
                    if (cursorIndex == 0)
                    {
                        // 몬스터 배열을 순회하며 가장 낮은 인덱스의 생존 몬스터를 찾습니다.
                        for (int i = 0; i < monsters.Count; i++)
                        {
                            if (monsters[i].IsDead == false)
                            {
                                cursorIndex = i - 1;
                                key = ConsoleKey.DownArrow;
                                break;
                            }
                        }
                    }

                    // 다음 메뉴로 이동시키기 위함임
                    ReadArrowKey(key);
                    return -2;
                }

                Console.SetCursorPosition(cursorLeft, contentTop + cursorIndex);
                WriteColor("▶", ConsoleColor.DarkCyan);
            }

            else if (key == ConsoleKey.DownArrow)
            {
                Console.SetCursorPosition(cursorLeft, contentTop + cursorIndex);
                Console.Write(' ');

                cursorIndex = Math.Min(cursorIndex + 1, CursorMenu.Count - 1);
                if (Program.CurrentScene is Scene_BattleSelect)
                {
                    // 나가기 버튼이 아니고, 아래 버튼 몬스터가 죽었다면
                    if (cursorIndex < monsters.Count && monsters[cursorIndex].IsDead)
                    {
                        // 한 번 더 아래로 재귀
                        ReadArrowKey(key);
                        return -2;
                    }
                }

                // 나가기 메뉴(마지막 인덱스) 간격 조절 여부에 따라 커서 위치 설정
                if (cursorIndex == CursorMenu.Count - 1 && offsetTop > 0)
                {
                    Console.SetCursorPosition(cursorLeft, contentTop + offsetTop);
                }
                else
                {
                    Console.SetCursorPosition(cursorLeft, contentTop + cursorIndex);
                }

                WriteColor("▶", ConsoleColor.DarkCyan);
            }

            else if (key == ConsoleKey.Enter)
            {
                CursorMenu[cursorIndex].Item2?.Invoke();
                return 0;
            }

            return -2;
        }

        // 인덱스 입력을 받는 함수 (예외처리 포함)
        public static int ReadIndex(bool hasZero = true)
        {
            while (true)
            {
                // 키 입력 받기
                WriteColor("\n >> ", ConsoleColor.DarkYellow);
                char input = Console.ReadKey(true).KeyChar;

                // 정수인지 검사
                if (char.IsDigit(input) == false)
                {
                    Console.Write(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                // 인덱스를 초과하는지 검사
                int index = input - '0';
                if (index > Program.CurrentScene.MenuCount)
                {
                    Console.Write(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                // 인덱스 0을 허용하는지 검사
                if (hasZero == false && index == 0)
                {
                    Console.Write(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                return index;
            }
        }

        // 텍스트를 애니메이션으로 그림니다. 구두점(.)은 더 느리게 재생됩니다!
        public static void WriteAnim(string value, ConsoleColor color = ConsoleColor.Gray)
        {
            (int left, int top) = Console.GetCursorPosition();
            Console.WriteLine();
            Console.SetCursorPosition(left, top);

            AudioManager.Instance.Play(AudioClip.SoundFX_WriteAnim);

            string[] texts = value.Split("name");
            for (int i = 0; i < texts.Length; i++)
            {
                if (i > 0)
                {
                    char[] name = Scene_CreatePlayer.PlayerName.ToCharArray();
                    foreach (char c in name)
                    {
                        WriteColor(c, ConsoleColor.DarkYellow);
                        Thread.Sleep(20);
                    }
                }

                char[] charArray = texts[i].ToCharArray();
                foreach (char c in charArray)
                {
                    WriteColor(c, color);

                    int delay = c == '.' ? 200 : 20;
                    Thread.Sleep(delay);
                }
            }
            Console.WriteLine();

            ClearBuffer();
            AudioManager.Instance.Stop(AudioClip.SoundFX_WriteAnim);
        }

        // 텍스트에 색상을 입힐 수 있습니다.
        public static void WriteColor(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }

        // 텍스트에 색상을 입히고 개행문자(\n)을 추가합니다.
        public static void WriteColorLine(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        // 텍스트에 name 단어가 포함되어 있으면 플레이어 이름으로 치환해줍니다.
        public static void WriteName(string value, ConsoleColor color = ConsoleColor.Gray)
        {
            string[] texts = value.Split("name");

            Console.Write(texts[0]);
            for (int i = 1; i < texts.Length; i++)
            {
                WriteColor(Scene_CreatePlayer.PlayerName, ConsoleColor.DarkYellow);
                WriteColor(texts[i], color);
            }
            Console.WriteLine();
        }

        // 애플리케이션을 종료시킵니다.
        public static void Quit()
        {
            Console.WriteLine("게임이 종료되었습니다.");

#pragma warning disable CS8625 // Null 리터럴을 null을 허용하지 않는 참조 형식으로 변환할 수 없습니다.
            Program.CurrentScene = null;
#pragma warning restore CS8625 // Null 리터럴을 null을 허용하지 않는 참조 형식으로 변환할 수 없습니다.
        }
    }
}