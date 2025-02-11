namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleLobby : Scene_BattleScene
    {

        private readonly Random random = new Random();
        private readonly List<Monster> monsters = GameManager.Instance.Monsters;

        public override void Awake()
        {
            Console.Clear();
            base.Awake();
            hasZero = false;

            Utils.CursorMenu.Add(("질문시작", () => Program.CurrentScene = new Scene_BattleMonsterSelect()));
            Utils.CursorMenu.Add(("돌아가기", () => {
                Program.CurrentScene = new Scene_Lobby();
                Program.PreviousScene = Program.CurrentScene;
                monsters.Clear();
            }));

            if (monsters.Count == 0)
            {
                MakeMonster();
            }
        }

        public override void LateStart()
        {
            base.LateStart();
        }
        protected override void Display()
        {
            DisplayBattleVS();//전투 VS 화면 출력(여기서 한번)
            GameManager.Instance.DisplayBattle();
        }

        public override int Update()
        {
            Utils.DisplayCursorMenu(5, 22);
            if(base.Update() == 0)
            {
                Utils.ClearLine(0, 22);
                Utils.ClearLine(0, 23);
                Utils.CursorMenu.Clear();
                return 0;
            }
            return 0;
        }

        // 랜덤 2-4명의 몬스터 생성
        private void MakeMonster()
        {
            if (monsters.Count > 0)
            {
                return;
            }

            int monsterCount = random.Next(2, 5);
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