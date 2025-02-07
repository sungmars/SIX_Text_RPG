namespace SIX_Text_RPG
{
    public enum MonsterType
    {
        염예찬,
        강성훈,
        김재경,
        이승환,
        이성언,
        김명민,
        김영호,
        김현정,
        강인,
        오정환,
        권관우,
        소재철,
        김주안,
        Count
    }

    internal class Monster : Creature
    {
        public Monster(MonsterType type)
        {
            Type = type;

            Stats stats = Define.MONSTERS_STATS[(int)type];
            stats.MaxHP = stats.HP;
            Stats = stats;
        }

        public MonsterType Type { get; private set; }

        //몬스터 정보 출력
        public void DisplayMonster()
        {
            if (IsDead)
            {
                //몬스터가 죽었을 때 컬러 (의논 필요)
                Console.WriteLine($" Lv.{Stats.Level} {Stats.Name} Dead");
            }
            else
            {
                Console.Write($" Lv.{Stats.Level} {Stats.Name}  ");
                Display_StatusBar(Stats.HP, Stats.MaxHP, ConsoleColor.DarkRed);
            }
        }
    }
}