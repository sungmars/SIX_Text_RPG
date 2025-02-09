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
        public Player(PlayerType type)
        {
            Type = type;

            Stats stats = Define.PLAYERS_STATS[(int)type];
            stats.Name = Scene_CreatePlayer.PlayerName;
            stats.MaxEXP = Define.PLAYER_EXP_TABLE[stats.Level - 1];
            stats.MaxHP = stats.HP;
            stats.MaxMP = stats.MP;
            Stats = stats;
        }

        public PlayerType Type { get; private set; }

        public char Graphic_Weapon { get; private set; } = 'つ';

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
            Display_EXPBar();
            int test = Console.CursorLeft;
            Console.WriteLine($" {Stats.EXP}/{Stats.MaxEXP}");

            Console.Write($" 체  력: ");
            Display_HPBar();
            Console.WriteLine($" {Stats.HP:F0}/{Stats.MaxHP:F0}");

            Console.Write($" 마  력: ");
            Display_MPBar();
            Console.WriteLine($" {Stats.MP:F0}/{Stats.MaxMP:F0}\n");
        }

        public override void Display_EXPBar()
        {
            base.Display_EXPBar();
            Utils.EXPBarY = Console.CursorTop;
        }

        public override void Display_HPBar()
        {
            base.Display_HPBar();
            Utils.HPBarY = Console.CursorTop;
        }

        public override void Display_MPBar()
        {
            base.Display_MPBar();
            Utils.MPBarY = Console.CursorTop;
        }

        public void DisplayInfo_Gold()
        {
            Console.Write($" 소지금:");
            Utils.WriteColorLine($" {Stats.Gold:N0}G", ConsoleColor.Yellow);
        }
    }
}