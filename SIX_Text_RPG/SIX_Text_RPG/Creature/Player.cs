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

        public void DisplayInfo_Gold()
        {
            Console.Write($" 소지금:");
            Utils.WriteColorLine($" {Stats.Gold}G", ConsoleColor.Yellow);
        }
    }
}