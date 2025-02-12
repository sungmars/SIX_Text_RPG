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
            Utils.CursorMenu.Add(("일반 질문", () => Program.CurrentScene = new Scene_BattleSelect()));
            Utils.CursorMenu.Add(("이상한 질문", () => Program.CurrentScene = new Scene_BattleSkill()));
            Utils.CursorMenu.Add(("가방 살펴보기", () => Program.CurrentScene = new Scene_BattleInventory()));
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

        // 랜덤 2-4명의 몬스터 생성
        private void SpawnMonster()
        {
            int monsterCount = random.Next(2, 4 + stage / 2);
            int sumLevel;

            Stats monsterStats;

            for (int i = 0; i < monsterCount; i++)
            {
                bool isSame = true;
                MonsterType type = (MonsterType)random.Next(1, Define.MONSTERS_STATS.Length);
                while (isSame)
                {
                    type = (MonsterType)random.Next(1, Define.MONSTERS_STATS.Length);
                    if (monsters.Count != 0)
                    {
                        foreach (var monster in monsters)
                        {
                            if (monster.Type == type)
                            {
                                isSame = true;
                                break;
                            }
                            else
                            {
                                isSame = false;
                            }
                        }
                    }
                    else
                    {
                        isSame = false;
                    }
                }
                monsters.Add(new Monster(type));
            }
            switch (stage)
            {

                case 0:
                    sumLevel = random.Next(5, 8);
                    break;
                case 1:
                    sumLevel = random.Next(11, 14);
                    break;
                case 2:
                    sumLevel = random.Next(17, 20);
                    break;
                case 3:
                    sumLevel = random.Next(23, 26);
                    break;
                case 4:
                    sumLevel = random.Next(29, 32);
                    break;
                default:
                    sumLevel = 0;
                    break;
            }

            foreach (var monster in monsters)
            {
                if (monsters.IndexOf(monster) == monsters.Count - 1)
                {
                    monsterStats =monster.Stats;
                    sumLevel = Math.Max(stage + 1, sumLevel);
                    monsterStats.Level = sumLevel;
                    monsterStats.ATK = sumLevel;
                    monsterStats.DEF = sumLevel / 2f;
                    monsterStats.EXP = sumLevel * 3;
                    monsterStats.Gold = 50 + sumLevel * 5;
                    monsterStats.MaxHP = sumLevel * 10;
                    monsterStats.HP = monsterStats.MaxHP;
                    monster.Stats = monsterStats;
                    break;
                }
                else
                {
                    monsterStats = monster.Stats;
                    int level = random.Next(stage + 1, Math.Max(stage + 2, sumLevel - 1 - stage));
                    sumLevel -= level;
                    monsterStats.Level = level;
                    monsterStats.ATK = level;
                    monsterStats.DEF = level / 2f;
                    monsterStats.EXP = level * 3;
                    monsterStats.Gold = 50 + level * 5;
                    monsterStats.MaxHP = level * 10;
                    monsterStats.HP = monsterStats.MaxHP;
                    monster.Stats = monsterStats;
                }
            }
        }
    }
}