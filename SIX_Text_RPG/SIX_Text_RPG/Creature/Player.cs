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
        private static readonly char WEAPON = 'つ';
        private static readonly char FACE = 'ω';

        #region StatusPosition
        private readonly int OFFSET_X = 11;
        private readonly int OFFSET_Y = 1;
        private readonly int STATUS_BAR_X = 30;
        private readonly int STATUS_GOLD_X = 9;
        private readonly int STATUS_LEVEL_X = 4;
        private readonly int STATUS_POWER_X = 9;

        private int levelY;
        private int expY;
        private int hpY;
        private int mpY;
        private int atkY;
        private int defY;
        private int goldY;
        #endregion

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
        public Stats EquipStats { get; private set; }
        public ConsoleColor Color_Weapon { get; private set; } = ConsoleColor.Yellow;
        public char Graphic_Weapon { get; private set; } = WEAPON;
        public char Grahpic_Face { get; private set; } = FACE;

        public void DisplayInfo(int startX = 0)
        {
            DisplayInfo_Status(startX);

            Console.SetCursorPosition(startX, Console.CursorTop);
            atkY = Console.CursorTop;
            Console.Write($" 공격력: {Stats.ATK} ");
            if (EquipStats.ATK > 0)
            {
                Utils.WriteColorLine($"+{EquipStats.ATK}", ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine();
            }

            Console.SetCursorPosition(startX, Console.CursorTop);
            defY = Console.CursorTop;
            Console.Write($" 방어력: {Stats.DEF} ");
            if (EquipStats.DEF > 0)
            {
                Utils.WriteColorLine($"+{EquipStats.DEF}\n", ConsoleColor.Green);
            }
            else
            {
                Console.WriteLine("\n");
            }

            Console.SetCursorPosition(startX, Console.CursorTop);
            DisplayInfo_Gold();
        }

        public void DisplayInfo_Status(int startX = 0)
        {
            levelY = Console.CursorTop;

            Console.SetCursorPosition(startX, Console.CursorTop);
            Console.WriteLine($" Lv.{Stats.Level:00}");

            Console.SetCursorPosition(startX, Console.CursorTop);
            Utils.WriteColor($" {Type}", ConsoleColor.DarkCyan);
            Utils.WriteColorLine($" {Stats.Name}\n", ConsoleColor.DarkYellow);

            Utils.ClearLine(startX, Console.CursorTop, 50);
            Console.SetCursorPosition(startX, Console.CursorTop);
            Console.Write($" 경험치: ");
            Display_EXPBar();
            Console.WriteLine($" {Stats.EXP}/{Stats.MaxEXP}");

            Utils.ClearLine(startX, Console.CursorTop, 50);
            Console.SetCursorPosition(startX, Console.CursorTop);
            Console.Write($" 체  력: ");
            Display_HPBar();
            Console.WriteLine($" {Stats.HP:F0}/{Stats.MaxHP:F0}");

            Utils.ClearLine(startX, Console.CursorTop, 50);
            Console.SetCursorPosition(startX, Console.CursorTop);
            Console.Write($" 마  력: ");
            Display_MPBar();
            Console.WriteLine($" {Stats.MP:F0}/{Stats.MaxMP:F0}\n");
        }

        public override void Display_EXPBar()
        {
            base.Display_EXPBar();
            expY = Console.CursorTop;
        }

        public override void Display_HPBar()
        {
            base.Display_HPBar();
            hpY = Console.CursorTop;
        }

        public override void Display_MPBar()
        {
            base.Display_MPBar();
            mpY = Console.CursorTop;
        }

        public void DisplayInfo_Gold()
        {
            goldY = Console.CursorTop;
            Console.Write($" 소지금:");
            Utils.WriteColorLine($" {Stats.Gold:N0}G", ConsoleColor.Yellow);
        }

        public void Equip(IEquipable? equipment)
        {
            if (equipment == null)
            {
                return;
            }

            if (equipment is not Item item)
            {
                return;
            }

            if (equipment is Weapon weapon)
            {
                Graphic_Weapon = weapon.Iteminfo.Graphic;
                Color_Weapon = weapon.Iteminfo.Color;
            }

            if (equipment is Accessory accessory)
            {
                Grahpic_Face = accessory.Iteminfo.Graphic;
            }

            // 아이템 스탯 적용 해제
            Stats -= EquipStats;

            Stats equipStats = EquipStats;
            ItemInfo info = item.Iteminfo;
            equipStats.ATK += info.ATK;
            equipStats.DEF += info.DEF;
            equipStats.MaxHP += info.MaxHP;
            equipStats.MaxMP += info.MaxMP;

            // 아이템 스탯 적용
            EquipStats = equipStats;
            Stats += EquipStats;
        }

        public void Unequip(IEquipable? equipment)
        {
            if (equipment == null)
            {
                return;
            }

            if (equipment is not Item item)
            {
                return;
            }

            if (equipment is Weapon)
            {
                Graphic_Weapon = WEAPON;
                Color_Weapon = ConsoleColor.Yellow;
            }

            if (equipment is Accessory)
            {
                Grahpic_Face = FACE;
            }

            // 아이템 스탯 적용 해제
            Stats -= EquipStats;

            Stats equipStats = EquipStats;
            ItemInfo info = item.Iteminfo;
            equipStats.ATK -= info.ATK;
            equipStats.DEF -= info.DEF;
            equipStats.MaxHP -= info.MaxHP;
            equipStats.MaxMP -= info.MaxMP;

            // 아이템 스탯 적용
            EquipStats = equipStats;
            Stats += EquipStats;
        }

        public void Render()
        {
            Console.SetCursorPosition(Position.X - OFFSET_X, Position.Y - OFFSET_Y);
            Console.WriteLine($" (´◎{Grahpic_Face}◎)");
            Console.Write(" (       つ");
        }

        public void Render_Heal(bool isPotion = false)
        {
            if (isPotion)
            {
                AudioManager.Instance.Play(AudioClip.SoundFX_Potion);
            }

            Console.SetCursorPosition(Position.X - OFFSET_X, Position.Y - OFFSET_Y);
            Utils.WriteColorLine($" (◎{Grahpic_Face}◎)", ConsoleColor.Green);
            Utils.WriteColor(" (       つ", ConsoleColor.Green);
            Thread.Sleep(100);

            Render();
        }

        public void Render_Hit()
        {
            AudioManager.Instance.Play(AudioClip.SoundFX_Damage1 + random.Next(0, 4));
            Console.SetCursorPosition(Position.X - OFFSET_X, Position.Y - OFFSET_Y);
            Utils.WriteColorLine($" (´＞{Grahpic_Face}＜)", ConsoleColor.Red);
            Utils.WriteColor(" (       つ", ConsoleColor.Red);
            Thread.Sleep(200);

            Render();
        }

        public void StatusAnim(Stat type, int amount)
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

            // type에 따라 현재값과 최대값을 설정합니다.
            int posX = 0;
            int posY = 0;
            float currentValue = 0;
            float maxValue = 0;
            switch (type)
            {
                case Stat.Level:
                    posX = STATUS_LEVEL_X;
                    posY = levelY;
                    currentValue = player.Stats.Level;
                    maxValue = int.MaxValue;
                    break;
                case Stat.ATK:
                    posX = STATUS_POWER_X;
                    posY = atkY;
                    currentValue = player.Stats.ATK;
                    maxValue = int.MaxValue;
                    break;
                case Stat.DEF:
                    posX = STATUS_POWER_X;
                    posY = defY;
                    currentValue = player.Stats.DEF;
                    maxValue = int.MaxValue;
                    break;
                case Stat.EXP:
                    posX = STATUS_BAR_X;
                    posY = expY;
                    currentValue = player.Stats.EXP;
                    maxValue = int.MaxValue;
                    break;
                case Stat.HP:
                    posX = STATUS_BAR_X;
                    posY = hpY;
                    currentValue = player.Stats.HP;
                    maxValue = player.Stats.MaxHP;
                    break;
                case Stat.MP:
                    posX = STATUS_BAR_X;
                    posY = mpY;
                    currentValue = player.Stats.MP;
                    maxValue = player.Stats.MaxMP;
                    break;
                case Stat.Gold:
                    posX = STATUS_GOLD_X;
                    posY = goldY;
                    currentValue = player.Stats.Gold;
                    maxValue = int.MaxValue;
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
                int delay = 0;
                switch (type)
                {
                    case Stat.HP:
                    case Stat.MP:
                        delay = 10;
                        break;
                    case Stat.Gold:
                        delay = 2;
                        break;
                    default:
                        delay = 50;
                        break;
                }

                Thread.Sleep(delay);
                Utils.ClearLine(posX, posY);

                switch (type)
                {
                    case Stat.Level:
                        Console.Write($"{currentValue:00} ");
                        break;
                    case Stat.Gold:
                        Utils.WriteColor($"{Stats.Gold:N0}G ", ConsoleColor.Yellow);
                        break;
                    default:
                        Console.Write($"{currentValue:f0} ");
                        break;
                }

                Utils.WriteColor("-> ", ConsoleColor.DarkYellow);

                value = (int)(currentValue + index * direction);
                if (type == Stat.Gold)
                {
                    Utils.WriteColor($"{value:N0}G", ConsoleColor.Yellow);
                }
                else
                {
                    Utils.WriteColor(value, color);
                }

                amount -= direction;
                index++;
            }

            // 커서를 원래 위치로 돌려놓습니다.
            Console.SetCursorPosition(left, top);
        }
    }
}