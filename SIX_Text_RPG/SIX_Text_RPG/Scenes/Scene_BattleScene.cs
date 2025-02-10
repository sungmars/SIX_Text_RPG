using System.Numerics;
using System;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleScene : Scene_Base
    {

        private readonly int LEFT = 99;
        private readonly int TOP = 5;
        private readonly string[] VERSUS =
        {
            " /$$    /$$  /$$$$$$   ",
            "| $$   | $$ /$$__  $$  ",
            "| $$   | $$| $$  \\__/ ",
            "|  $$ / $$/|  $$$$$$   ",
            " \\  $$ $$/  \\____  $$",
            "  \\  $$$/   /$$  \\ $$",
            "   \\  $/   |  $$$$$$/ ",
            "    \\_/     \\______/\n",
        };

        private readonly Random random = new Random();
        protected List<Monster> monsters = GameManager.Instance.Monsters;
        private readonly Player? player = GameManager.Instance.Player;

        protected readonly int CURSORMENU_TOP = 22;

        protected bool isPlayAnim = false;


        public override void Awake()
        {
            hasZero = false;

            Utils.WriteColorLine("\n 질문 VS 피드백\n\n", ConsoleColor.DarkYellow);
        }

        public override void LateStart()
        {
            base.LateStart();
            Console.WriteLine();


            Program.PreviousScene = Program.CurrentScene;
        }

        public override int Update()
        {
            return base.Update();
        }

        protected override void Display()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            WaitDisplay();

            // 플레이어 정보 출력
            Console.SetCursorPosition(0, 5);
            player.DisplayInfo_Status();

            WaitDisplay();

            // 현재 커서위치 저장
            (int left, int top) = Console.GetCursorPosition();

            // VS 아스키 텍스트 출력
            for (int i = 0; i < VERSUS.Length; i++)
            {
                Console.SetCursorPosition(50, TOP + i);
                Utils.WriteColorLine(VERSUS[i], i > 2 ? ConsoleColor.DarkRed : ConsoleColor.Red);
            }


            // 모든 몬스터 정보 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                WaitDisplay();

                Console.SetCursorPosition(LEFT, TOP + 6 - i);
                monsters[i].DisplayMonster();
            }

            WaitDisplay();

            Console.WriteLine();

            // 커서 위치 초기화
            Console.SetCursorPosition(left, top);
            
            Console.WriteLine();

            Utils.DisplayLine(!(Program.PreviousScene is Scene_BattleScene), 3);
            
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            GameManager.Instance.DisplayBattle();

        }

        protected bool PlayerPhase(int selectMonsterNum)
        {

            if (player == null)
            {
                return true;
            }


            Console.SetCursorPosition(0, 16);
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
                damageActions[i] = () =>player.Damaged(damage);
                if(player.Stats.HP > 0) 
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

        private void WaitDisplay()
        {
            if (!(Program.PreviousScene is Scene_BattleScene))
            {
                Thread.Sleep(400);
            }
        }

    }
}