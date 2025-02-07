namespace SIX_Text_RPG
{
    public enum MonsterType
    {
        YYC,    // 염예찬 튜터님
        KSH,    // 강성훈 튜터님
        KJK,    // 김재경 튜터님
        LSH,    // 이승환 튜터님
        LSE,    // 이성언 튜터님
        KMM,    // 김명민 튜터님
        KYH,    // 김영호 튜터님
        KHJ,    // 김현정 튜터님
        KI,     // 강  인 튜터님
        OJH,    // 오정환 튜터님
        KKW,    // 권관우 튜터님
        SJC,    // 소재철 튜터님
        KJA,    // 김주안 튜터님
        Count
    }

    internal class Monster : Creature
    {
        public Monster(MonsterType type) : base()
        {
            Type = type;
            Stats = Define.MONSTERS_STATS[(int)type];
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