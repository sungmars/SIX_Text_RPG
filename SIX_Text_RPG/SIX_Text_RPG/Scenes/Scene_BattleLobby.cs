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

            // 기본 공격 횟수
            reservedSkill = null;
            attackCount = 1;
        }
        public override int Update()
        {
            if (base.Update() == 0)
            {
                if (Program.CurrentScene is Scene_BattleInventory || Program.CurrentScene is Scene_BattleSkill)//가방 살펴보기에 들어갈때 배틀 로비 메뉴가 출력되도록
                {
                    Console.SetCursorPosition(0, CURSOR_TOP);
                    Utils.WriteColorLine("    ", ConsoleColor.Gray);
                    Console.SetCursorPosition(0, CURSOR_TOP + 1);
                    Utils.WriteColorLine("    ", ConsoleColor.Gray);
                    Console.SetCursorPosition(0, CURSOR_TOP + 2);
                    Utils.WriteColorLine("    ", ConsoleColor.Gray);
                    Console.SetCursorPosition(0, CURSOR_TOP + 3);
                    Utils.WriteColorLine("    ", ConsoleColor.DarkGray);
                }
                else if (Program.CurrentScene is Scene_BattleSelect)
                {
                    for (int i = 0; i < Utils.CursorMenu.Count; i++)
                    {
                        Utils.ClearLine(0, CURSOR_TOP + i);
                    }
                }

                    Utils.CursorMenu.Clear();
                return 0;
            }
            else
            {

            }

            return -1;
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
                    sumLevel = random.Next(18, 20);
                    break;
                case 3:
                    sumLevel = random.Next(25, 28);
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
                    monsterStats.ATK = sumLevel * 2f;
                    monsterStats.DEF = sumLevel /1.5f;
                    monsterStats.EXP = sumLevel * 4;
                    monsterStats.Gold = 50+ sumLevel * 200;
                    monsterStats.MaxHP = sumLevel * 15;
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
                    monsterStats.ATK = level * 2f;
                    monsterStats.DEF = level / 1.5f;
                    monsterStats.EXP = level * 4;
                    monsterStats.Gold = 50 + level * 200;
                    monsterStats.MaxHP = level * 15;
                    monsterStats.HP = monsterStats.MaxHP;
                    monster.Stats = monsterStats;
                }
            }
        }
    }
}