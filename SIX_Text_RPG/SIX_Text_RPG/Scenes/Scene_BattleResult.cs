using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleResult : Scene_Base
    {
        private bool isVictory;
        private 
        public Scene_BattleResult(bool isVictory)
        {
            this.isVictory = isVictory;
        }
        public override void Awake()
        {
            sceneTitle = "Battle!! - Result";
            sceneInfo = "";
            base.Awake();
        }

        public override int Update()
        {
            switch (base.Update())
            { 
                case 0:
                    Program.CurrentScene = new Scene_Title();
                    break;

                default:
                    break;
            }
            return 0;
        }

        protected override void Display()
        {
            if (isVictory)
            {
                Utils.WriteColorLine("Victory", ConsoleColor.Green);
                //몬스터 처치수 수정할 것
                Console.WriteLine($"\n\n던전에서 몬스터 {0} 마리를 잡았습니다.");
            }

            else
            {
                Utils.WriteColorLine("You Lose...", ConsoleColor.DarkRed);
            }
            //플레이어 이름과 레벨, 체력 나중에 수정할 것
            Console.WriteLine($"\n\nLv. {1} {"James"}");
            Console.WriteLine($"\n\nHP {1} -> {0}");
            Console.WriteLine("\n 0. 다음");
        }
    }
}
