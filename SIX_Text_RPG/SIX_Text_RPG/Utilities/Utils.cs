﻿using SIX_Text_RPG.Scenes;

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

        // 애니메이션(Thread.Sleep) 재생 중인 동안 눌린 키를 지워줌.
        public static void ClearBuffer()
        {
            Thread.Sleep(100);
            while (Console.KeyAvailable) Console.ReadKey(true);
        }

        // 원하는 콘솔 좌표 시작점 left로부터 top 좌표에 있는 모든 텍스트를 지워준다.
        public static void ClearLine(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', Console.WindowWidth - left));
            Console.SetCursorPosition(left, top);
        }

        // 커서 메뉴를 출력합니다.
        public static void DisplayCursorMenu(int left, int top)
        {
            cursorIndex = 0;
            contentLeft = left;
            contentTop = top;

            Console.SetCursorPosition(left - 3, top);
            WriteColor("▶", ConsoleColor.DarkCyan);

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
        }

        // 화면에 선을 그립니다. 원할 경우 애니메이션, 이중선도 그릴 수 있습니다.
        public static void DisplayLine(bool hasAnim = false, int secondLineTop = 0)
        {
            int width = Console.WindowWidth;
            int firstX = 0;
            int firstY = Console.CursorTop + 1;
            int secondX = width - 2;

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
                    Console.Write("  ");

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

        // 플레이어 상태(HP, MP, EXP) 텍스트 애니메이션 (회복, 감소 둘 다 가능!)
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

        // 텍스트를 애니메이션으로 그림니다. 구두점(.)은 더 느리게 재생됩니다!
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