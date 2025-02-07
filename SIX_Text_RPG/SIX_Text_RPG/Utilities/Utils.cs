using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Utils
    {
        public static void ClearBuffer()
        {
            Thread.Sleep(100);
            while (Console.KeyAvailable) Console.ReadKey(true);
        }

        public static int ReadIndex(bool hasZero = true)
        {
            while (true)
            {
                WriteColor("\n >> ", ConsoleColor.DarkYellow);

                char input = Console.ReadKey(true).KeyChar;
                if (char.IsDigit(input) == false)
                {
                    Console.Write(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                int index = input - '0';
                if (index > Program.CurrentScene.MenuCount)
                {
                    Console.Write(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                if (hasZero == false && index == 0)
                {
                    Console.WriteLine(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                return index;
            }
        }

        public static void WriteAnim(string value)
        {
            char[] charArray = value.ToCharArray();
            foreach (char c in charArray)
            {
                Console.Write(c);

                int delay = c == '.' ? 500 : 100;
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public static void WriteColor(string value, ConsoleColor color)
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

        public static void WriteMenuLine(string value)
        {
            string[] texts = value.Split("\n");

            Console.WriteLine(texts[0]);
            for (int i = 1; i < texts.Length; i++)
            {
                WriteColorLine($"     {texts[i]}", ConsoleColor.DarkGray);
            }
        }

        public static void WriteName(string value)
        {
            string[] texts = value.Split("name");

            if (value.StartsWith("name"))
            {
                WriteColor(Scene_CreatePlayer.PlayerName, ConsoleColor.DarkYellow);
            }
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
            Program.CurrentScene = null;
        }
    }
}