namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattlePhase : Scene_Battle
    {
        public override void Awake()
        {
            hasZero = false;
        }

        public override int Update()
        {
            if (PlayerPhase(monsterIndex))
            {
                // 플레이어 승리 로직
                Program.CurrentScene = new Scene_BattleResult();
                QuestManager.Instance.UpdateQuestProgress(0,monsters.Count);
                return 0;
            }

            Thread.Sleep(500);

            if (MonsterPhase())
            {
                // 플레이어 패배 로직
                Program.CurrentScene = new Scene_BattleResult();
                return 0;
            }

            Program.CurrentScene = new Scene_BattleLobby();
            return 0;
        }

        public override void LateStart() { }

        protected bool PlayerPhase(int selectMonsterNum)
        {
            if (player == null)
            {
                return true;
            }

            GameManager.Instance.DisplayBattle_Attack(selectMonsterNum, 6, () =>
            {
                // 플레이어 공격
                float damage = CalculateDamage(player.Stats.ATK);
                if (Utils.LuckyMethod(15))
                {
                    //치명타
                    damage = (damage * 1060f) / 100f;
                }
                GameManager.Instance.Monsters[selectMonsterNum].Damaged(damage);
                QuestManager.Instance.KillCountPlus(1, (int)GameManager.Instance.Monsters[selectMonsterNum].Type);
            });

            // 몬스터가 1마리라도 살아있으면 false
            foreach (var monster in monsters)
            {
                if (monster.Stats.HP > 0)
                {
                    return false;
                }
            }

            // 플레이어 승리
            return true;
        }

        protected bool MonsterPhase()
        {
            if (player == null)
            {
                return true;
            }

            Action[] damageActions = new Action[monsters.Count];

            for (int i = 0; i < monsters.Count; i++)
            {
                float currentHP = player.Stats.HP;
                float damage = CalculateDamage(monsters[i].Stats.ATK);
                damageActions[i] = () =>
                {
                    if(Utils.LuckyMethod(50))
                    {
                        //회피
                        damage = 0;
                    }
                    player.Damaged(damage);
                    Display_PlayerInfo();
                    if (player.Stats.HP > 0)
                    {
                        GameManager.Instance.TotalDamage += damage;
                    }
                    else
                    {
                        GameManager.Instance.TotalDamage += currentHP;
                    }
                };

                
            }

            GameManager.Instance.DisplayBattle_Damage(damageActions);
            return player.IsDead;
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