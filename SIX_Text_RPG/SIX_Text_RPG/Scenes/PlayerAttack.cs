using System;
using System.Collections.Generic;
using System.Numerics;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_PlayerAttack : Scene_Base
    {
        private List<Monster> monsters;
        
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
                Console.Write($"{i + 1}. \n");
                if (monsters[i].IsDead)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                monsters[i].DisplayMonster();
                Console.ResetColor();
            }
        }
        
    }
}
