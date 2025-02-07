namespace SIX_Text_RPG
{
    internal class Utils
    {
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

        // 글자 애니메이션
    }
}