using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattlePhase : Scene_BattleDisplay
    {

        private Random random = new Random();
        private List<Monster> monsters = GameManager.Instance.Monsters;
        private Player? player = GameManager.Instance.Player;

        private int selectMonsterNum = 0;

        private int left = 0;
        private int top = 0;

        public Scene_BattlePhase(int selectMonsterNum)
        {
            this.selectMonsterNum = selectMonsterNum;
        }


        public override void Awake()
        {
            base.Awake();
            hasZero = false;
            zeroText = "다음";
        }

        public override int Update()
        {
            hasZero = true;
            if (PlayerPhase(selectMonsterNum))
            {
                Program.CurrentScene = new Scene_BattleResult(true);
                return 0;
            }
            (left, top) = Console.GetCursorPosition();
            Console.WriteLine($"\n [0] player{zeroText}");//player다음
            if (base.Update() == 0)
            {
                MonsterPhase();
                if (player != null && player.Stats.HP <= 0)
                {
                    // 플레이어 사망시 결과로
                    Program.CurrentScene = new Scene_BattleResult(false);
                    return 0;
                }
                Console.SetCursorPosition(left,top);
                Console.WriteLine($"\n [0] monster{zeroText}");//monster다음
                if (base.Update() == 0)
                {
                    Program.CurrentScene = new Scene_BattleLobby();
                    return 0;
                }
                Program.CurrentScene = new Scene_BattleMonsterSelect();
                return 0;
                
            }
            else
            {
                return 1;
            }
        }

        protected override void Display()
        {
            base.Display();
        }

        private bool PlayerPhase(int monsterNum)
        {

            if (player == null)
            {
                return true;
            }

            Console.SetCursorPosition(0, 16);
            //GameManager.Instance.DisplayBattle(monsterNum, 6, () =>
            //{
            //    // 플레이어 공격
            //    GameManager.Instance.Monsters[monsterNum].Damaged(CalculateDamage(player.Stats.ATK));
            //});

            foreach (var monster in monsters)
            {
                if (monster.Stats.HP > 0)
                {
                    return false;
                }
            }
            return true;
        }

        private void MonsterPhase()
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
                float damage = CalculateDamage(monster.Stats.ATK);
                float beforeHP = player.Stats.HP;
                player.Damaged(damage);
                Console.WriteLine(damage);
                if (player.Stats.HP > 0)
                {
                    GameManager.Instance.TotalDamage += damage;
                }
                else
                {
                    GameManager.Instance.TotalDamage += beforeHP;
                }
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
