namespace SIX_Text_RPG
{
    public enum MonsterType
    {
        None
    }

    internal class Monster : Creature
    {
        public MonsterType Type { get; private set; }
        public Monster(MonsterType type, string name, int level, float atk, float def, float hp, int gold) : base(name, level, atk, def, hp, gold)
        {
            Type = type;
        }

        public void DisplayMonster()
        {
            if (IsDead)
            {
                Console.WriteLine($"Lv.{Stats.Level} {Stats.Name} Dead");
            }
            else
            {
                Console.WriteLine($"Lv.{Stats.Level} {Stats.Name}  HP {Stats.HP}");
            }
        }
    }
}