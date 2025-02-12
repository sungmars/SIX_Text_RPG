using SIX_Text_RPG.Skills;

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

            if (reservedSkill != null)
            {
                // 플레이어 정보 갱신
                player.SetStat(Stat.MP, -reservedSkill.Mana, true);
                base.Display();
            }

            if (reservedSkill is Skill_WideAttack)
            {
                selectMonsterNum = -1;
            }

            int totalCount = attackCount;
            GameManager.Instance.DisplayBattle_Attack(selectMonsterNum, attackCount, () =>
            {
                int monsterIndex = selectMonsterNum == -1 ? Math.Abs(attackCount - totalCount) / 2 : selectMonsterNum;
                attackCount--;

                // 플레이어 공격
                float damage = CalculateDamage(player.Stats.ATK);
                damage -= monsters[monsterIndex].Stats.DEF;
                if (damage <= 1)
                {
                    damage = 1;
                }

                // 예약된 스킬이 없다면 (스킬은 회피 불가)
                if (reservedSkill == null)
                {
                    // 몬스터 회피
                    var avoid = monsters[monsterIndex].Skills.Find(x => x.GetType() == typeof(Skill_Avoid));
                    if (avoid != null && avoid.Skill())
                    {
                        damage = 0;
                    }
                }

                // 피흡이 가능한 스킬이라면
                if (reservedSkill is Skill_StrangeAttack || reservedSkill is Skill_WideAttack)
                {
                    player.SetStat(Stat.HP, damage * 0.2f, true);
                    player.Render_Heal();
                    base.Display();
                }

                // 데미지 및 퀘스트 갱신
                GameManager.Instance.Monsters[monsterIndex].Damaged(damage);
                QuestManager.Instance.KillCountPlus(1, (int)GameManager.Instance.Monsters[monsterIndex].Type);
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
                    if (damage <= 1)
                    {
                        damage = 1;
                    }

                    player.Damaged(damage);
                    Display_PlayerInfo();
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