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
            else
            {

            }

            return 0;
        }

        // 랜덤 2-4명의 몬스터 생성
        private void SpawnMonster()
        {
            int monsterCount = random.Next(2, 4 + stage/2);
            int sumLevel;

            for (int i = 0; i < monsterCount; i++)
            {
                MonsterType type = (MonsterType)random.Next(1, Define.MONSTERS_STATS.Length);
                monsters.Add(new Monster(type));
            }
            switch (stage)
            {

                case 0:
                    sumLevel = random.Next(4, 8);
                    break;
                case 1:
                    sumLevel = random.Next(10, 15);
                    break;
                case 2:
                    sumLevel = random.Next(16, 21);
                    break; 
                case 3:
                    sumLevel = random.Next(22, 27);
                    break; 
                case 4:
                    sumLevel = random.Next(28, 33);
                    break;
                default:
                    sumLevel = 0;
                    break;
            }

            foreach (var monster in monsters)
            {
                if (monsters.IndexOf(monster) == monsters.Count - 1)
                {
                    monster.SetStat(Stat.Level, sumLevel);
                    monster.SetStat(Stat.ATK, sumLevel);
                    monster.SetStat(Stat.EXP, 0 + sumLevel * 2);
                    monster.SetStat(Stat.Gold, 20 + sumLevel * 3);
                    break;
                }
                else
                {
                    int level = random.Next(stage + 1, sumLevel-(monsterCount -1)*(stage + 1));
                    sumLevel -= (level - stage);
                    monster.SetStat(Stat.Level, level);
                    monster.SetStat(Stat.ATK, level);
                    monster.SetStat(Stat.EXP, 0 + level * 2);
                    monster.SetStat(Stat.Gold, 20 + level * 3);
                }
            }
        }
    }
}