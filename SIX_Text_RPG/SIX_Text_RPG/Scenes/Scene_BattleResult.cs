using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleResult : Scene_Base
    {
        public override void Awake()
        {
            sceneTitle = "Battle!! - Result";
            sceneInfo = string.Empty;
            base.Awake();
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentScene = new Scene_BattleLobby();
                    break;

                default:
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Utils.WriteColorLine("Vivtory", ConsoleColor.Green);
            Console.WriteLine($"\n던전에서 몬스터 { 0 }마리를 잡았습니다.");
            Console.WriteLine($"\nLv. {1} {"James"}");
            Console.WriteLine();
        }
    }
}
