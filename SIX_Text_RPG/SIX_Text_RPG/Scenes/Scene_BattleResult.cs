using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleResult : Scene_Base
    {
        private bool isVictory;

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
            List<Monster> monster = new List<Monster>();
            monster = Scene_BattleStart.GetMonsters();

            if (isVictory)
            {
                Utils.WriteColorLine("Victory", ConsoleColor.Green);
                //몬스터 처치 수 수정할 것
                Console.WriteLine($"\n\n던전에서 몬스터 {monster.Count} 마리를 잡았습니다.");
            }

            else
            {
                Utils.WriteColorLine("You Lose...", ConsoleColor.DarkRed);
            }
            Console.WriteLine($"\n\nLv. {GameManager.Instance.Player.Stats.Level} {GameManager.Instance.Player.Stats.Name}");
            Console.WriteLine($"\n\nHP {GameManager.Instance.Player.Stats.HP} -> {GameManager.Instance.Player.Stats.HP}");
            Console.WriteLine("\n 0. 다음");
        }
    }
}
