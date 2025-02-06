namespace SIX_Text_RPG
{
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
        public Creature() { }
        public Creature(string name, int level, float atk, float def, float hp, int gold)
        {
            Stats = new()
            {
                Name = name,
                Level = level,
                ATK = atk,
                DEF = def,
                HP = hp,
                MaxHP = hp,
                Gold = gold
            };
        }

        public bool IsDead { get { return Stats.HP == 0; } }
        public Stats Stats { get; set; }

        public void Damaged(float damage)
        {
            Stats stats = Stats;

            float hp = stats.HP - damage;
            stats.HP = MathF.Max(hp, 0);
            stats.HP = MathF.Min(hp, stats.MaxHP);

            Stats = stats;
        }
    }
}