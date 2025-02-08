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
            Position = new() { X = Define.MONSTER_SPAWN_X };

            Stats stats = Define.MONSTERS_STATS[(int)type];
            stats.MaxHP = stats.HP;
            Stats = stats;

            Random random = new();
            char eye = Define.EYES_MONSTER[random.Next(0, Define.EYES_MONSTER.Length)];
            char nose = Define.FACES[random.Next(0, Define.FACES.Length)];
            graphic = $"({eye}{nose}{eye}) ";
        }

        public MonsterType Type { get; private set; }

        private readonly string graphic;

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
            }
        }

        public void Render()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(graphic);
        }

        public void Render_Hit()
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Utils.WriteColor(graphic, ConsoleColor.Red);
            Thread.Sleep(200);

            Render();
        }
    }
}