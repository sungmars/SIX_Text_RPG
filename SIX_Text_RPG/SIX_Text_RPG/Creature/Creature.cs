namespace SIX_Text_RPG
{
    public enum Stat
    {
        Level,
        ATK,
        DEF,
        MaxHP,
        Gold
    }

    public struct Stats
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public float ATK { get; set; }
        public float DEF { get; set; }
        public float HP { get; set; }
        public float MaxHP { get; set; }

        public int Gold { get; set; }
    }

    internal abstract class Creature
    {
        public Creature()
        {
            Stats stats = Stats;
            stats.MaxHP = stats.HP;
            Stats = stats;
        }

        public Stats Stats { get; set; }
        public bool IsDead { get { return Stats.HP == 0; } }

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
                case Stat.MaxHP:
                    stats.MaxHP = isRelative ? stats.MaxHP + value : value;
                    break;
                case Stat.Gold:
                    stats.Gold = isRelative ? stats.Gold + (int)value : (int)value;
                    break;
            }

            Stats = stats;
        }

        public void Damaged(float damage)
        {
            Stats stats = Stats;
            float hp = stats.HP - damage;

            // 체력이 0 미만으로 설정되지 않습니다.
            hp = MathF.Max(hp, 0);

            // 체력이 최대 체력 이상으로 설정되지 않습니다.
            hp = MathF.Min(hp, stats.MaxHP);

            stats.HP = hp;
            Stats = stats;
        }

        protected void Display_StatusBar(float value, float maxValue, ConsoleColor color)
        {
            (int left, int top) = Console.GetCursorPosition();

            // 전체 상태바를 그립니다.
            Utils.WriteColor("[][][][][][][][][][]", ConsoleColor.DarkGray);
            Console.WriteLine($" {value}/{maxValue}");

            bool barDirection = true;
            int barCount = IsDead ? 0 : (int)MathF.Max(Stats.HP / Stats.MaxHP * 20, 1);

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

            Console.SetCursorPosition(0, top + 1);
        }
    }
}