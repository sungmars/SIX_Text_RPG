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

            Utils.ClearBuffer();
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
                return 0;
            }

            return -1;
        }

        // 랜덤 2-4명의 몬스터 생성
        private void SpawnMonster()
        {
            int monsterCount = random.Next(2, 4 + stage/2);

            for (int i = 0; i < monsterCount; i++)
            {
                MonsterType type = (MonsterType)random.Next(1, Define.MONSTERS_STATS.Length);
                monsters.Add(new Monster(type));
            }
            switch (stage)
            {
                case 0:
                    foreach (var monster in monsters)
                    {
                        int level = random.Next(1, 4);
                        monster.SetStat(Stat.Level,level);
                        monster.SetStat(Stat.ATK, level);
                        monster.SetStat(Stat.EXP, 0 + level * 2);
                        monster.SetStat(Stat.Gold, 8 + level * 2);
                    }
                    break;
                case 1:
                    foreach (var monster in monsters)
                    {
                        int level = random.Next(3, 6);
                        monster.SetStat(Stat.Level, level);
                        monster.SetStat(Stat.ATK, level * 2);
                        monster.SetStat(Stat.EXP, 0 + level * 3);
                        monster.SetStat(Stat.Gold, 8 + level * 3);
                    }
                    break;
                case 2:
                    foreach (var monster in monsters)
                    {
                        int level = random.Next(5, 8);
                        monster.SetStat(Stat.Level, level);
                        monster.SetStat(Stat.ATK, level * 3);
                        monster.SetStat(Stat.EXP, 0 + level * 4);
                        monster.SetStat(Stat.Gold, 8 + level * 4);
                    }
                    break; 
                case 3:
                    foreach (var monster in monsters)
                    {
                        int level = random.Next(7, 10);
                        monster.SetStat(Stat.Level, level);
                        monster.SetStat(Stat.ATK, level * 4);
                        monster.SetStat(Stat.EXP, 0 + level * 5);
                        monster.SetStat(Stat.Gold, 8 + level * 5);
                    }
                    break; 
                case 4:
                    foreach (var monster in monsters)
                    {
                        int level = random.Next(9, 12);
                        monster.SetStat(Stat.Level, level);
                        monster.SetStat(Stat.ATK, 3 + level * 5);
                        monster.SetStat(Stat.EXP, 0 + level * 6);
                        monster.SetStat(Stat.Gold, 8 + level * 6);
                    }
                    break;
            }
        }
    }
}