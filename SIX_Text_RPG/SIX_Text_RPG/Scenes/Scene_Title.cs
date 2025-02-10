﻿namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Title : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();
            hasZero = false;

            Utils.CursorMenu.Add(("새로운 찌르기", () => Program.CurrentScene = new Scene_CreatePlayer()));
            Utils.CursorMenu.Add(("익숙한 찌르기", () =>
            {
                if (DataManager.Instance.LoadData() == false)
                {
                    Console.ReadKey();
                    return;
                }

                Program.CurrentScene = new Scene_Lobby();
            }
            ));
            Utils.CursorMenu.Add(("찌르고 나가기", () =>
            {
                Utils.WriteColor("\n\n >> ", ConsoleColor.DarkYellow);
                Utils.Quit();
            }
            ));

            //AudioManager.Instance.PlayMusic("Music_Title");
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

            Utils.DisplayCursorMenu(103, Console.CursorTop - 2);
        }
    }
}