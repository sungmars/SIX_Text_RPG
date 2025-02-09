namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Title : Scene_Base
    {
        private readonly int LEFT = 103;

        private int cursorIndex;
        private int cursorTop;

        public override void Awake()
        {
            base.Awake();
            hasZero = false;
        }

        public override int Update()
        {
            int cursor = LEFT - 3;
            while (Program.CurrentScene == this)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Console.Write(' ');

                    cursorIndex = Math.Max(cursorIndex - 1, 0);
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Utils.WriteColor("▶", ConsoleColor.DarkCyan);
                }

                if (key.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Console.Write(' ');

                    cursorIndex = Math.Min(cursorIndex + 1, 2);
                    Console.SetCursorPosition(cursor, cursorTop + cursorIndex);
                    Utils.WriteColor("▶", ConsoleColor.DarkCyan);
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    switch (cursorIndex)
                    {
                        case 0:
                            Program.CurrentScene = new Scene_CreatePlayer();
                            break;
                        case 1:
                            // TODO: 이어하기
                            Scene_CreatePlayer.PlayerName = "테스트플레이어";
                            Program.CurrentScene = new Scene_CreateCharacter();
                            break;
                        case 2:
                            Console.SetCursorPosition(0, cursorTop + 4);
                            Utils.WriteColor("\n >> ", ConsoleColor.DarkYellow);
                            Utils.Quit();
                            break;
                    }
                }
            }

            return 0;
        }

        protected override void Display()
        {
            Console.SetCursorPosition(0, 0);

            Utils.WriteColor(
                "\n    /$$$$$$  /$$$$$$$   /$$$$$$   /$$$$$$  /$$   /$$ /$$$$$$$$ /$$$$$$$$ /$$$$$$$$ /$$$$$$    " +
                "\n   /$$__  $$| $$__  $$ /$$__  $$ /$$__  $$| $$  | $$| $$_____/|__  $$__/|__  $$__/|_  $$_/    " +
                "\n  | $$  \\__/| $$  \\ $$| $$  \\ $$| $$  \\__/| $$  | $$| $$         | $$      | $$     | $$  " +
                "\n  |  $$$$$$ | $$$$$$$/| $$$$$$$$| $$ /$$$$| $$$$$$$$| $$$$$      | $$      | $$     | $$      " +
                "\n   \\____  $$| $$____/ | $$__  $$| $$|_  $$| $$__  $$| $$__/      | $$      | $$     | $$     " +
                "\n   /$$  \\ $$| $$      | $$  | $$| $$  \\ $$| $$  | $$| $$         | $$      | $$     | $$    " +
                "\n  |  $$$$$$/| $$      | $$  | $$|  $$$$$$/| $$  | $$| $$$$$$$$   | $$      | $$    /$$$$$$    " +
                "\n   \\______/ |__/      |__/  |__/ \\______/ |__/  |__/|________/   |__/      |__/   |______/\n", ConsoleColor.White);

            Utils.WriteColor(
                "\n    /$$$$$$   /$$$$$$  /$$$$$$$  /$$$$$$ /$$   /$$  /$$$$$$   /$$$$$$  /$$       /$$   /$$ /$$$$$$$        " +
                "\n   /$$__  $$ /$$__  $$| $$__  $$|_  $$_/| $$$ | $$ /$$__  $$ /$$__  $$| $$      | $$  | $$| $$__  $$       " +
                "\n  | $$  \\__/| $$  \\ $$| $$  \\ $$  | $$  | $$$$| $$| $$  \\__/| $$  \\__/| $$      | $$  | $$| $$  \\ $$ " +
                "\n  | $$      | $$  | $$| $$  | $$  | $$  | $$ $$ $$| $$ /$$$$| $$      | $$      | $$  | $$| $$$$$$$        " +
                "\n  | $$      | $$  | $$| $$  | $$  | $$  | $$  $$$$| $$|_  $$| $$      | $$      | $$  | $$| $$__  $$       " +
                "\n  | $$    $$| $$  | $$| $$  | $$  | $$  | $$\\  $$$| $$  \\ $$| $$    $$| $$      | $$  | $$| $$  \\ $$    " +
                "\n  |  $$$$$$/|  $$$$$$/| $$$$$$$/ /$$$$$$| $$ \\  $$|  $$$$$$/|  $$$$$$/| $$$$$$$$|  $$$$$$/| $$$$$$$/      " +
                "\n   \\______/  \\______/ |_______/ |______/|__/  \\__/ \\______/  \\______/ |________/ \\______/ |_______/\n", ConsoleColor.DarkYellow);

            Utils.WriteColor("\n" +
                "\n   /$$$$$$$$ /$$$$$$$$ /$$   /$$ /$$$$$$$$       /$$$$$$$  /$$$$$$$   /$$$$$$    " +
                "\n  |__  $$__/| $$_____/| $$  / $$|__  $$__/      | $$__  $$| $$__  $$ /$$__  $$   " +
                "\n     | $$   | $$      |  $$/ $$/   | $$         | $$  \\ $$| $$  \\ $$| $$  \\__/" +
                "\n     | $$   | $$$$$    \\  $$$$/    | $$         | $$$$$$$/| $$$$$$$/| $$ /$$$$  " +
                "\n     | $$   | $$__/     >$$  $$    | $$         | $$__  $$| $$____/ | $$|_  $$   " +
                "\n     | $$   | $$       /$$/\\  $$   | $$         | $$  \\ $$| $$      | $$  \\ $$" +
                "\n     | $$   | $$$$$$$$| $$  \\ $$   | $$         | $$  | $$| $$      |  $$$$$$/  " +
                "\n     |__/   |________/|__/  |__/   |__/         |__/  |__/|__/       \\______/   ", ConsoleColor.DarkRed);

            Console.SetCursorPosition(LEFT, Console.CursorTop - 2);
            cursorTop = Console.CursorTop;

            Utils.WriteColorLine("새로운 찌르기", ConsoleColor.White);
            Console.SetCursorPosition(LEFT, Console.CursorTop);
            Utils.WriteColorLine("익숙한 찌르기", ConsoleColor.White);
            Console.SetCursorPosition(LEFT, Console.CursorTop);
            Utils.WriteColorLine("찌르고 나가기", ConsoleColor.DarkGray);

            Console.SetCursorPosition(LEFT - 3, cursorTop);
            Utils.WriteColor("▶", ConsoleColor.DarkCyan);
        }
    }
}