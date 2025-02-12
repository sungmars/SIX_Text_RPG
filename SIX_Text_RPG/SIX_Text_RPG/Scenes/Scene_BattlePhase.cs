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
                QuestManager.Instance.UpdateQuestProgress(0, monsters.Count);
                return 0;
            }

            // 몬스터 정보 갱신
            base.Display();

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

            // 기본 공격 횟수
            int attackCount = 1;

            // 크리티컬 발동 시 두배 공격
            var critical = player.Skills.Find(x => x.GetType() == typeof(Skill_Critical));
            if (critical != null && critical.Skill())
            {
                attackCount *= 2;
            }

            GameManager.Instance.DisplayBattle_Attack(selectMonsterNum, attackCount, () =>
            {
                // 플레이어 공격
                float damage = CalculateDamage(player.Stats.ATK);
                damage -= monsters[selectMonsterNum].Stats.DEF;
                if (damage <= 1)
                {
                    damage = 1;
                }

                // 몬스터 회피
                var avoid = monsters[selectMonsterNum].Skills.Find(x => x.GetType() == typeof(Skill_Avoid));
                if (avoid != null && avoid.Skill())
                {
                    damage = 0;
                }

                // 데미지 및 퀘스트 갱신
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

            Action?[] damageActions = new Action[monsters.Count];
            for (int i = 0; i < monsters.Count; i++)
            {
                var skill = player.Skills.Find(x => x.GetType() == typeof(Skill_Avoid));
                if (skill != null && skill.Skill())
                {
                    damageActions[i] = null;
                    continue;
                }

                float currentHP = player.Stats.HP;
                float damage = CalculateDamage(monsters[i].Stats.ATK);
                damageActions[i] = () =>
                {
                    damage -= player.Stats.DEF;
                    if(damage <= 1)
                    {
                        damage = 1;
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