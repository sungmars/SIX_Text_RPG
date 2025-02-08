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
        public Vector2 Position { get; set; }
        public Stats Stats { get; set; }
        public bool IsDead { get { return Stats.HP == 0; } }

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

        public void Display_EXPBar() => Display_StatusBar(Stats.HP / Stats.MaxHP, ConsoleColor.DarkGreen);
        public void Display_HealthBar() => Display_StatusBar(Stats.HP / Stats.MaxHP, ConsoleColor.DarkRed);
        public void Display_ManaBar() => Display_StatusBar(Stats.HP / Stats.MaxHP, ConsoleColor.Blue);

        private void Display_StatusBar(float percentage, ConsoleColor color)
        {
            (int left, int top) = Console.GetCursorPosition();

            // 전체 상태바를 그립니다.
            Utils.WriteColor("[][][][][][][][][][]", ConsoleColor.DarkGray);

            bool barDirection = true;
            int barCount = IsDead ? 0 : (int)MathF.Max(percentage * 20, 1);

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
                case Stat.MaxHP:
                    stats.MaxHP = isRelative ? stats.MaxHP + value : value;
                    break;
                case Stat.Gold:
                    stats.Gold = isRelative ? stats.Gold + (int)value : (int)value;
                    break;
            }

            Stats = stats;
        }
    }
}