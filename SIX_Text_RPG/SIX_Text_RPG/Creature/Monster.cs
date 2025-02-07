namespace SIX_Text_RPG
{
    public enum MonsterType
    {
        None
    }

    internal class Monster : Creature
    {
        public MonsterType Type { get; private set; }

        //몬스터 생성자
        public Monster(MonsterType type, string name, int level, float atk, float def, float hp, int gold) : base(name, level, atk, def, hp, gold)
        {
            Type = type;
        }

        //몬스터 정보 출력
        public void DisplayMonster()
        {
            if (IsDead)
            {
                //몬스터가 죽었을 때 컬러 (의논 필요)
                Console.WriteLine($"Lv.{Stats.Level} {Stats.Name} Dead");
            }
            else
            {
                Console.Write($"Lv.{Stats.Level} {Stats.Name}  ");
                Display_StatusBar(Stats.HP, Stats.MaxHP, ConsoleColor.DarkRed);
            }
        }
    }
}