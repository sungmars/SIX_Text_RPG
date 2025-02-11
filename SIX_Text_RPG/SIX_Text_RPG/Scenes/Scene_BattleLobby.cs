namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleLobby : Scene_Battle
    {
        private readonly int CURSOR_TOP = 23;

        public override void Awake()
        {
            if (HasAnim)
            {
                Console.Clear();
                base.Awake();
            }

            Utils.CursorMenu.Add(("질문시작", () => Program.CurrentScene = new Scene_BattleSelect()));
            Utils.CursorMenu.Add(("돌아가기", () =>
            {
                monsters.Clear();
                Program.CurrentScene = new Scene_Lobby();
            }
            ));

            if (monsters.Count == 0)
            {
                SpawnMonster();
            }
        }

        protected override void Display()
        {
            if (HasAnim)
            {
                base.Display();
                GameManager.Instance.DisplayBattle();
            }

            Utils.DisplayCursorMenu(4, CURSOR_TOP);
        }

        public override int Update()
        {
            if (base.Update() == 0)
            {
                for (int i = 0; i < Utils.CursorMenu.Count; i++)
                {
                    Utils.ClearLine(0, CURSOR_TOP + i);
                }

                Utils.CursorMenu.Clear();
            }

            return 0;
        }

        // 랜덤 2-4명의 몬스터 생성
        private void SpawnMonster()
        {
            int monsterCount = random.Next(2, 5);
            for (int i = 0; i < monsterCount; i++)
            {
                MonsterType type = (MonsterType)random.Next(1, Define.MONSTERS_STATS.Length);
                monsters.Add(new Monster(type));
            }
        }
    }
}