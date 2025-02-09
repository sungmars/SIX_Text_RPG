using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Utils
    {
        private static readonly int STATUS_BAR_X = 29;

        public static List<(string, Action)> CursorMenu { get; private set; } = new();

        private static readonly Random random = new();

        private static int cursorIndex;
        private static int contentLeft;
        private static int contentTop;

        public static void ClearBuffer()
        {
            Thread.Sleep(100);
            while (Console.KeyAvailable) Console.ReadKey(true);
        }

        public static void ClearLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', Console.WindowWidth - left));
            Console.SetCursorPosition(left, top);
        }

        public static void DisplayCursorMenu(int left, int top)
        {
            cursorIndex = 0;
            contentLeft = left;
            contentTop = top;

            Console.SetCursorPosition(left, top);

            for (int i = 0; i < CursorMenu.Count; i++)
            {
                Console.SetCursorPosition(left, Console.CursorTop);
                if (i == CursorMenu.Count - 1)
                {
                    WriteColorLine(CursorMenu[i].Item1, ConsoleColor.DarkGray);
                }
                else
                {
                    WriteColorLine(CursorMenu[i].Item1, ConsoleColor.Gray);
                }
            }

            Console.SetCursorPosition(left - 3, top);
            WriteColor("▶", ConsoleColor.DarkCyan);
        }

        public static void DisplayLine(bool hasAnim = false)
        {
            int x = 0;
            int y = Console.CursorTop;
            Console.SetCursorPosition(x, y + 1);

            int width = Console.WindowWidth;
            while (x < width)
            {
                Console.Write("〓");
                x += 2;

                if (hasAnim)
                {
                    Thread.Sleep(1);
                }
            }
            Console.WriteLine();
        }

        public static bool LuckyMethod(int percent)
        {
            return random.Next(1, 101) <= percent;
        }

        public static int ReadArrow()
        {
            int cursorLeft = contentLeft - 3;
            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(cursorLeft, contentTop + cursorIndex);
                    Console.Write(' ');

                    cursorIndex = Math.Max(cursorIndex - 1, 0);
                    Console.SetCursorPosition(cursorLeft, contentTop + cursorIndex);
                    WriteColor("▶", ConsoleColor.DarkCyan);
                }

                else if (input.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(cursorLeft, contentTop + cursorIndex);
                    Console.Write(' ');

                    cursorIndex = Math.Min(cursorIndex + 1, CursorMenu.Count - 1);
                    Console.SetCursorPosition(cursorLeft, contentTop + cursorIndex);
                    WriteColor("▶", ConsoleColor.DarkCyan);
                }

                else if (input.Key == ConsoleKey.Enter)
                {
                    CursorMenu[cursorIndex].Item2?.Invoke();
                    break;
                }
            }

            return 0;
        }

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

        public static void StatusAnim(int statusBarY, int amount)
        {
            if (amount == 0)
            {
                return;
            }

            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            (int left, int top) = Console.GetCursorPosition();

            // StatusBar type를 지정합니다.
            int type = 0;
            if (statusBarY == player.EXPBarY) type = 1;
            else if (statusBarY == player.HPBarY) type = 2;
            else if (statusBarY == player.MPBarY) type = 3;

            // type에 따라 현재값과 최대값을 설정합니다.
            float currentValue = 0;
            float maxValue = 0;
            switch (type)
            {
                case 1:
                    currentValue = player.Stats.EXP;
                    maxValue = int.MaxValue;
                    break;
                case 2:
                    currentValue = player.Stats.HP;
                    maxValue = player.Stats.MaxHP;
                    break;
                case 3:
                    currentValue = player.Stats.MP;
                    maxValue = player.Stats.MaxMP;
                    break;
            }

            // value가 이미 maxVlaue라면 종료합니다.
            int direction = amount > 0 ? 1 : -1;
            if (direction == 1 && currentValue == maxValue)
            {
                return;
            }

            // direction에 따라 애니메이션 텍스트 색상을 지정합니다.
            int value = direction;
            ConsoleColor color = direction == 1 ? ConsoleColor.Green : ConsoleColor.Red;

            // amount를 모두 회복하거나, value가 0 미만이거나, maxValue 만큼 회복하면 종료합니다.
            int index = 1;
            while (amount != 0 && value != 0 && value != maxValue)
            {
                Thread.Sleep(20);
                ClearLine(STATUS_BAR_X, statusBarY);

                Console.Write($" {currentValue} ");
                WriteColor("-> ", ConsoleColor.DarkYellow);

                value = (int)(currentValue + index * direction);
                WriteColor(value, color);

                amount -= direction;
                index++;
            }

            // 커서를 원래 위치로 돌려놓습니다.
            Console.SetCursorPosition(left, top);
        }

        public static void WriteAnim(string value, ConsoleColor color = ConsoleColor.Gray)
        {
            (int left, int top) = Console.GetCursorPosition();
            Console.WriteLine();
            Console.SetCursorPosition(left, top);

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
        }

        public static void WriteColor(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }

        public static void WriteColorLine(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }

        public static void WriteName(string value)
        {
            string[] texts = value.Split("name");

            Console.Write(texts[0]);
            for (int i = 1; i < texts.Length; i++)
            {
                WriteColor(Scene_CreatePlayer.PlayerName, ConsoleColor.DarkYellow);
                Console.Write(texts[i]);
            }
            Console.WriteLine();
        }

        public static void Quit()
        {
            Console.WriteLine("게임이 종료되었습니다.");

#pragma warning disable CS8625 // Null 리터럴을 null을 허용하지 않는 참조 형식으로 변환할 수 없습니다.
            Program.CurrentScene = null;
#pragma warning restore CS8625 // Null 리터럴을 null을 허용하지 않는 참조 형식으로 변환할 수 없습니다.
        }
    }
}