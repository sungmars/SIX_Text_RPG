namespace SIX_Text_RPG
{
    public enum Stat
    {
        Level,
        ATK,
        DEF,
        EXP,
        MaxEXP,
        HP,
        MaxHP,
        MP,
        MaxMP,
        Gold
    }

    public struct Stats
    {
        public static Stats operator +(Stats s1, Stats s2)
        {
            Stats stats = new()
            {
                Name = s1.Name,
                Level = s1.Level + s2.Level,
                ATK = s1.ATK + s2.ATK,
                DEF = s1.DEF + s2.DEF,
                EXP = s1.EXP + s2.EXP,
                MaxEXP = s1.MaxEXP + s2.MaxEXP,
                HP = s1.HP + s2.HP,
                MaxHP = s1.MaxHP + s2.MaxHP,
                MP = s1.MP + s2.MP,
                MaxMP = s1.MaxMP + s2.MaxMP,
                Gold = s1.Gold + s2.Gold
            };

            return stats;
        }

        public static Stats operator -(Stats s1, Stats s2)
        {
            Stats stats = new()
            {
                Name = s1.Name,
                Level = s1.Level - s2.Level,
                ATK = s1.ATK - s2.ATK,
                DEF = s1.DEF - s2.DEF,
                EXP = s1.EXP - s2.EXP,
                MaxEXP = s1.MaxEXP - s2.MaxEXP,
                HP = s1.HP - s2.HP,
                MaxHP = s1.MaxHP - s2.MaxHP,
                MP = s1.MP - s2.MP,
                MaxMP = s1.MaxMP - s2.MaxMP,
                Gold = s1.Gold - s2.Gold
            };

            return stats;
        }

        public string Name { get; set; }
        public int Level { get; set; }

        public float ATK { get; set; }
        public float DEF { get; set; }

        public int EXP { get; set; }
        public int MaxEXP { get; set; }
        public float HP { get; set; }
        public float MaxHP { get; set; }
        public float MP { get; set; }
        public float MaxMP { get; set; }

        public int Gold { get; set; }
    }

    internal abstract class Creature
    {
        public Vector2 Position { get; set; }
        public Stats Stats { get; set; }
        public bool IsDead { get { return Stats.HP == 0; } }

        protected Random random = new();

        public void Damaged(float damage)
        {
            Stats stats = Stats;
            float hp = stats.HP - damage;

            // 체력이 0 미만으로 설정되지 않습니다.
            stats.HP = MathF.Max(hp, 0);

            // 체력이 최대 체력 이상으로 설정되지 않습니다.
            stats.HP = MathF.Min(stats.HP, Stats.MaxHP);

            Stats = stats;
        }

        public virtual void Display_EXPBar()
        {
            if (Stats.MaxEXP == 0)
            {
                Display_StatusBar(0.0f, ConsoleColor.DarkGreen);
                return;
            }

            Display_StatusBar(MathF.Min((float)Stats.EXP / Stats.MaxEXP, 1.0f), ConsoleColor.DarkGreen);
        }

        public virtual void Display_HPBar() => Display_StatusBar(MathF.Min(Stats.HP / Stats.MaxHP, 1.0f), ConsoleColor.DarkRed);
        public virtual void Display_MPBar() => Display_StatusBar(MathF.Min(Stats.MP / Stats.MaxMP, 1.0f), ConsoleColor.Blue);

        private void Display_StatusBar(float percentage, ConsoleColor color)
        {
            (int left, int top) = Console.GetCursorPosition();

            // 전체 상태바를 그립니다.
            Utils.WriteColor("[][][][][][][][][][]", ConsoleColor.DarkGray);
            int currentX = Console.CursorLeft;

            // 상태바 방향을 지정할 변수입니다.
            bool barDirection = true;

            // 상태바에 색상을 얼마나 채울 것인지 지정할 변수입니다.
            int barCount = (int)(percentage * 20);

            // 상태바를 순회하며 채워줍니다.
            while (barCount > 0)
            {
                Console.SetCursorPosition(left, top);
                if (barDirection) Utils.WriteColor("[", color);
                else Utils.WriteColor("]", color);

                left++;
                barDirection = !barDirection;
                barCount--;
            }

            Console.SetCursorPosition(currentX, top);
        }

        public void SetPosition(int x, int y)
        {
            Vector2 position = Position;
            position.X = x;
            position.Y = y;
            Position = position;
        }

        public void SetStat(Stat type, float value, bool isRelative = false)
        {
            Stats stats = Stats;

            switch (type)
            {
                case Stat.Level:
                    stats.Level = isRelative ? stats.Level + (int)value : (int)value;
                    break;
                case Stat.ATK:
                    stats.ATK = isRelative ? stats.ATK + value : value;
                    break;
                case Stat.DEF:
                    stats.DEF = isRelative ? stats.DEF + value : value;
                    break;
                case Stat.EXP:
                    stats.EXP = isRelative ? stats.EXP + (int)value : (int)value;
                    break;
                case Stat.MaxEXP:
                    stats.MaxEXP = isRelative ? stats.MaxEXP + (int)value : (int)value;
                    break;
                case Stat.HP:
                    if (isRelative)
                    {
                        Damaged(-value);
                        return;
                    }
                    else stats.HP = Math.Min(value, stats.MaxHP);
                    break;
                case Stat.MaxHP:
                    stats.MaxHP = isRelative ? stats.MaxHP + value : value;
                    break;
                case Stat.MP:
                    stats.MP = isRelative ? Math.Min(stats.MP + value, stats.MaxMP) : Math.Min(value, stats.MaxMP);
                    break;
                case Stat.MaxMP:
                    stats.MaxMP = isRelative ? stats.MaxMP + value : value;
                    break;
                case Stat.Gold:
                    stats.Gold = isRelative ? stats.Gold + (int)value : (int)value;
                    break;
            }

            Stats = stats;
        }
    }
}