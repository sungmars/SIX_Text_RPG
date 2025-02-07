using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    public enum PlayerType
    {
        마계조단,
        천계조단,
        Count
    }

    internal class Player : Creature
    {
        public Player(PlayerType type) : base()
        {
            Type = type;

            Stats stats = Define.PLAYERS_STATS[(int)type];
            stats.Name = Scene_CreatePlayer.PlayerName;
            stats.MaxHP = stats.HP;
            Stats = stats;
        }

        public PlayerType Type { get; private set; }

        public void DisplayInfo()
        {
            DisplayInfo_Status();

            Console.WriteLine($" 공격력: {Stats.ATK}");
            Console.WriteLine($" 방어력: {Stats.DEF}\n");

            DisplayInfo_Gold();
        }

        public void DisplayInfo_Status()
        {
            Console.WriteLine($" Lv.{Stats.Level:00}");
            Utils.WriteColor($" {Type}", ConsoleColor.DarkCyan);
            Utils.WriteColorLine($" {Stats.Name}\n", ConsoleColor.DarkYellow);

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
            Utils.WriteColorLine($" {Stats.Gold:N0}G", ConsoleColor.Yellow);
        }
    }
}