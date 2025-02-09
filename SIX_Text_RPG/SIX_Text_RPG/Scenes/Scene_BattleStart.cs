namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleStart : Scene_DisplayBattle
    {

        private readonly Random random = new Random();
        private readonly List<Monster> monsters = GameManager.Instance.Monsters;

        public override void Awake()
        {
            base.Awake();

            isSelectMonster = false;

            Menu.Add("공격");

            if(monsters.Count == 0)
            {
                MakeMonster();
            }
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentScene = new Scene_BattlePhase();
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            base.Display();
        }

        // 랜덤 1-4명의 몬스터 생성
        private void MakeMonster()
        {
            if (monsters.Count > 0)
            {
                return;
            }

            int monsterCount = random.Next(1, 5);
            for (int i = 0; i < monsterCount; i++)
            {
                monsters.Add(SetMonster(random.Next(1, 13)));
            }
        }

        // 3가지 종유릐 입력받은 값의 몬스터 설정
        private Monster SetMonster(int index)
        {
            return new Monster((MonsterType)index);
        }
    }
}