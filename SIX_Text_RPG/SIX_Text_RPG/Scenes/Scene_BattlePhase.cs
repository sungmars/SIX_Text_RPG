using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattlePhase : Scene_DisplayBattle
    {

        private Random random = new Random();
        private List<Monster> monsters = GameManager.Instance.Monsters;
        private Player? player = GameManager.Instance.Player;

        private int phaseType = 0;//0:몬스터 선택 페이즈, 1:공격 페이즈, 2:몬스터 공격 페이즈
        private int selectMonsterNum = 0;


        public override void Awake()
        {
            base.Awake();

            if(phaseType == 0)//몬스터 선택 페이즈
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    Menu.Add(string.Empty);
                }

                zeroText = "취소";
            }
            else // 공격 페이즈
            {
                zeroText = "다음";
            }   
        }

        public override int Update()
        {
            int selectNum;
            if (phaseType == 0)
            {
                selectNum = base.Update();
                if (selectNum == 0)
                {
                    Program.CurrentScene = new Scene_BattleStart();
                    return 0;
                }
                else
                {
                    phaseType = 1;
                    selectMonsterNum = selectNum - 1;
                    Awake();
                    Start();
                    return 1;
                }
            }
            if (phaseType == 1)
            {
                selectNum = base.Update();
                if (selectNum == 0)
                {
                    phaseType = 2;
                    Awake();
                    Start();
                    LateStart();
                    return 1;
                }
            }
            else if (phaseType == 2)
            {
                selectNum = base.Update();
                if (selectNum == 0)
                {
                    Program.CurrentScene = new Scene_BattlePhase();
                    return 0;
                }
            }
            return 1;
        }

        protected override void Display()
        {
            base.Display();
            if (phaseType == 0) {
                PlayerPhase(0);
            }

        private void PlayerPhase(int monsterNum)
        {
            if (player == null)
            {
                return;
            }
            if (monsters.Count == 0)
            {
                return;
            }

            Console.SetCursorPosition(0, 25);
            GameManager.Instance.DisplayBattle(monsterNum, 6, () =>
            {
                // 플레이어 공격
                GameManager.Instance.Monsters[monsterNum].Damaged(CalculateDamage(player.Stats.ATK));
            });
            
        }

        public void MonsterPhase()
        {
            if (player == null)
            {
                return;
            }
            if (monsters.Count == 0)
            {
                return;
            }

            foreach(var monster in monsters)
            {
                player.Damaged(CalculateDamage(monster.Stats.ATK));
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
