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
        private bool isVictory = false;
        private int monsterCount = 0;

        public Scene_BattleResult(bool isVictory)
        {
            this.isVictory = isVictory;
        }

        public Scene_BattleResult(bool isVictory, List<Monster> monsters)
        {
            this.isVictory = isVictory;
            this.monsterCount = monsters.Count;
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
                Utils.WriteColorLine(" Victory", ConsoleColor.Green);
                Console.WriteLine($"\n\n던전에서 몬스터 {monsterCount} 마리를 잡았습니다.");
            }
            else
            {
                Utils.WriteColorLine(" You Lose...", ConsoleColor.DarkRed);
            }
            Console.WriteLine($"\n\n Lv.{GameManager.Instance.Player.Stats.Level} {GameManager.Instance.Player.Stats.Name}");
            Console.WriteLine($" HP{GameManager.Instance.Player.Stats.HP} -> {GameManager.Instance.Player.Stats.HP}");
        }
    }
}
