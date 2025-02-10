using System.Numerics;
using System;

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
        private readonly List<Monster> monsters = GameManager.Instance.Monsters;
        private readonly Player? player = GameManager.Instance.Player;

        protected int selectMonsterNum = 0;
        protected bool isPlayAnim = false;


        public override void Awake()
        {
            base.Awake();
            hasZero = false;

            Utils.WriteColorLine("\n 질문 VS 피드백\n\n", ConsoleColor.DarkYellow);
        }

        public override void LateStart()
        {
            base.LateStart();
            Console.WriteLine();
            Console.WriteLine();
            Utils.DisplayLine(true, 3);
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

            // 플레이어 정보 출력
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            player.DisplayInfo_Status();

            // 현재 커서위치 저장
            (int left, int top) = Console.GetCursorPosition();

            // VS 아스키 텍스트 출력
            for (int i = 0; i < VERSUS.Length; i++)
            {
                Console.SetCursorPosition(50, TOP + i);
                Utils.WriteColorLine(VERSUS[i], i > 2 ? ConsoleColor.DarkRed : ConsoleColor.Red);
            }

            if(!isPlayAnim)
            {
                Thread.Sleep(500);
            }

            // 모든 몬스터 정보 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                if (!isPlayAnim)
                {
                    Thread.Sleep(500);
                }

                Console.SetCursorPosition(LEFT, TOP + 6 - i);
                monsters[i].DisplayMonster();
            }

            if (!isPlayAnim)
            {
                Thread.Sleep(500);
            }

            Console.WriteLine();

            // 커서 위치 초기화
            Console.SetCursorPosition(left, top);

            // 전투 장면 출력
            Console.SetCursorPosition(0, 15);
            DisplayGround();
            (left, top) = Console.GetCursorPosition();
            DisplayBatte_Monsters();

        }

        protected bool PlayerPhase()
        {

            if (player == null)
            {
                return true;
            }

            Console.SetCursorPosition(0, 16);
            GameManager.Instance.DisplayBattle(selectMonsterNum, 6, () =>
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

            foreach (var monster in monsters)
            {
                float damage = CalculateDamage(monster.Stats.ATK);
                float beforeHP = player.Stats.HP;
                player.Damaged(damage);
                if (player.Stats.HP > 0)
                {
                    GameManager.Instance.TotalDamage += damage;
                    return false;
                }
                else
                {
                    GameManager.Instance.TotalDamage += beforeHP;
                    return true;
                }
            }
            return false;
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

        protected void DisplayBattleGround()
        {
            // 전투장면 출력
           
        }

        private void DisplayGround()
        {
            if (player == null)
            {
                return;
            }
            int top = Console.CursorTop - 1;
            for (int i = 0; i < 4; i++)
            {
                Utils.ClearLine(0, top + i);
            }

            Console.SetCursorPosition(0, top);
            Console.WriteLine(" (´◎ω◎)");
            Console.Write(" (       つ");
            player.SetPosition(Console.CursorLeft, Console.CursorTop);

            Console.WriteLine();
            Utils.ClearLine(0, Console.CursorTop);
            Console.WriteLine();

        }

        private void DisplayBatte_Monsters()
        {
            int top = Console.CursorTop - 3;
            for (int i = 0; i < monsters.Count; i++)
            {
                monsters[i].SetPosition(monsters[i].Position.X, top);
                Console.SetCursorPosition(monsters[i].Position.X, top++);
                monsters[i].Render();
            }
        }

    }
}