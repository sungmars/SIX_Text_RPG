using System;
using System.Collections.Generic;
using System.Numerics;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_PlayerAttack : Scene_Base
    {
        private List<Monster> monsters;
        Player? player = GameManager.Instance.Player;
        private static Random random = new Random();

        //Monster 리스트를 전달받는 생성자
        public Scene_PlayerAttack(List<Monster> monsters)
        {
            this.monsters = monsters;
        }

        public override void Awake()
        {
            base.Awake();
            sceneTitle = "Battle!!";
            sceneInfo = "";

        }
       

        // Monster 리스트 출력
        protected override void Display()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                // 번호를 붙여 출력
                Console.Write($"{i + 1}. ");
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                monsters[i].DisplayMonster();
                Console.ResetColor();
                
                
            }
            if (player == null)
            {
                Console.WriteLine("\n플레이어 정보를 찾을 수 없습니다.");
            }
            else
            {
                player.DisplayInfo_Status();
            }

        }
        private int CalculateDamage(float baseATK)
        {
            int errorMargin = (int)MathF.Ceiling(baseATK * 0.1f);
            int minDamage = (int)baseATK - errorMargin;
            int maxDamage = (int)baseATK + errorMargin;
            if (minDamage < 0) minDamage = 0;
            return random.Next(minDamage, maxDamage + 1);
        }
        public override int Update()
        {
            int selection = -1;
            bool validSelection = false;

            do
            {
                Console.Write("대상을 선택해주세요.");
                selection = Utils.ReadIndex(hasZero);

                if (selection == 0)
                {
                    return 0;
                }
                if (selection < 1 || selection > monsters.Count)
                {
                    Utils.WriteColorLine("잘못된 입력입니다", ConsoleColor.Red);
                    continue;
                }
                Monster target = monsters[selection - 1];
                if (target.IsDead)
                {
                    Utils.WriteColorLine("잘못된 입력입니다", ConsoleColor.Red);
                    continue;
                }
                validSelection = true;
            } while (!validSelection);

            Monster monsterToAttack = monsters[selection - 1];
            float previousHP = monsterToAttack.Stats.HP;
            int damage = CalculateDamage(player.Stats.ATK);
            monsterToAttack.Damaged(damage);

            Console.Clear();
            Utils.WriteColorLine("Battle!!\n", ConsoleColor.DarkYellow);
            Utils.WriteColorLine($"{player.Stats.Name} 의 공격!", ConsoleColor.White);
            Utils.WriteColorLine(
                $"Lv.{monsterToAttack.Stats.Level} {monsterToAttack.Stats.Name} 을(를) 맞췄습니다. [데미지 : {damage}]\n",
                ConsoleColor.White);

            Console.Write($"Lv.{monsterToAttack.Stats.Level} {monsterToAttack.Stats.Name}\nHP {previousHP} -> ");
            if (monsterToAttack.IsDead)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("Dead");
                Console.ResetColor();
            }
            else
            {
                Console.Write(monsterToAttack.Stats.HP);
            }
            Console.WriteLine("\n");
            Utils.WriteColorLine("0. 다음", ConsoleColor.White);
            
            while (Console.ReadLine() != "0") { }

            


            return selection;
        

        }
    }
}
