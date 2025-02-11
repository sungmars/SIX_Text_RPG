namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattlePhase : Scene_Battle
    {
        public override void Awake()
        {
            hasZero = false;
            PlayerPhase(monsterIndex);
            MonsterNext();
        }

        public override int Update()
        {

            if (IsAllMonsterDead())
            {
                Program.CurrentScene = new Scene_BattleResult(true);
                return 0;
            }
            else
            {
                Utils.DisplayCursorMenu(5, CURSORMENU_TOP);
                if (base.Update() == 0)
                {
                    Utils.CursorMenu.Clear();
                    Utils.ClearLine(0, CURSORMENU_TOP);
                    return 0;
                }
                return 0;
            }
        }

        public override void LateStart()
        {
            Console.Write(string.Empty);
        }

        protected override void Display()
        {
            base.Display();
        }

        private void MonsterNext()
        {
            if (MonsterPhase())
            {
                Program.CurrentScene = new Scene_BattleResult(false);
            }
            else
            {
                Utils.CursorMenu.Clear();
                Utils.CursorMenu.Add(("monster다음", () => Program.CurrentScene = new Scene_BattleLobby()));
                Utils.DisplayCursorMenu(5, CURSORMENU_TOP);
                if (base.Update() == 0)
                {
                    Utils.CursorMenu.Clear();
                    Utils.ClearLine(0, CURSORMENU_TOP);
                }
            }
        }

        private bool IsAllMonsterDead()
        {
            return monsters.All(monster => monster.IsDead);
        }

        protected bool PlayerPhase(int selectMonsterNum)
        {
            if (player == null)
            {
                return true;
            }

            //Console.SetCursorPosition(0, 16);
            GameManager.Instance.DisplayBattle_Attack(selectMonsterNum, 6, () =>
            {
                // 플레이어 공격
                GameManager.Instance.Monsters[selectMonsterNum].Damaged(CalculateDamage(player.Stats.ATK));
            });

            foreach (var monster in monsters)
            {
                if (monster.Stats.HP > 0)
                {
                    return false;
                }
            }
            return true;
        }

        protected bool MonsterPhase()
        {
            if (player == null)
            {
                return true;
            }

            Action[] damageActions = new Action[monsters.Count];

            float damage = 0;

            float beforeHP = player.Stats.HP;

            for (int i = 0; i < monsters.Count; i++)
            {
                beforeHP = player.Stats.HP;
                damage = CalculateDamage(monsters[i].Stats.ATK);
                damageActions[i] = () => player.Damaged(damage);
                if (player.Stats.HP > 0)
                    GameManager.Instance.TotalDamage += damage;
                else
                    GameManager.Instance.TotalDamage += beforeHP;
            }

            GameManager.Instance.DisplayBattle_Damage(damageActions);

            if (player.Stats.HP <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private float CalculateDamage(float atk)
        {
            if (player == null)
            {
                return 0;
            }
            if (monsters.Count == 0)
            {
                return 0;
            }
            // -10% ~ 10% 랜덤 퍼센트
            float percent = random.Next(-10, 11);
            return atk + ((atk * percent) / 100.0f);
        }

    }
}