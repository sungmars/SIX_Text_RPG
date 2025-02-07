namespace SIX_Text_RPG
{
    public enum PlayerType
    {
        None
    }

    internal class Player : Creature
    {
        public PlayerType Type { get; private set; }

        public void SetGold(int gold)
        {
            Stats stats = Stats;
            stats.Gold += gold;
            Stats = stats;
        }

        public void DisplayInfo()
        {
            DisplayInfo_Status();

            Console.WriteLine($" 공격력: {Stats.ATK}");
            Console.WriteLine($" 방어력: {Stats.DEF}\n");

            DisplayInfo_Gold();
        }

        public void DisplayInfo_Status()
        {
            Utils.WriteColorLine($" Lv.{Stats.Level:00}", ConsoleColor.White);
            Utils.WriteColor($" {Stats.Name}", ConsoleColor.DarkYellow);
            Console.WriteLine($" {Type}\n", ConsoleColor.DarkGray);

            Console.Write($" 경험치: ");
            Display_StatusBar(Stats.HP, Stats.MaxHP, ConsoleColor.DarkGreen);
            Console.Write($" 체  력: ");
            Display_StatusBar(Stats.HP, Stats.MaxHP, ConsoleColor.Red);
            Console.Write($" 마  력: ");
            Display_StatusBar(Stats.HP, Stats.MaxHP, ConsoleColor.Blue);
            Console.WriteLine();
        }

        private void Display_StatusBar(float value, float maxValue, ConsoleColor color)
        {
            (int left, int top) = Console.GetCursorPosition();

            Utils.WriteColor("[][][][][][][][][][]", ConsoleColor.DarkGray);
            Console.WriteLine($" {value}/{maxValue}", ConsoleColor.White);

            int unit = IsDead ? 0 : (int)MathF.Max(Stats.HP / Stats.MaxHP * 20, 1);
            while (unit > 0)
            {
                Console.SetCursorPosition(left, top);
                if (left % 2 == 1) Utils.WriteColor("[", color);
                else Utils.WriteColor("]", color);

                left++;
                unit--;
            }

            Console.SetCursorPosition(0, top + 1);
        }

        public void DisplayInfo_Gold()
        {
            Console.Write($" 소지금:");
            Utils.WriteColorLine($" {Stats.Gold}G", ConsoleColor.Yellow);
        }
    }
}