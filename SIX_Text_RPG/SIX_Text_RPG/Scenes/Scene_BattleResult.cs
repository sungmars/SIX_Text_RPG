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
            //이전 체력 계산
            Func<float, float, float> beforeHP = (x, y) =>
                (x + y) > GameManager.Instance.Player.Stats.MaxHP ? GameManager.Instance.Player.Stats.MaxHP : x + y;
            
            float oldHP = beforeHP(GameManager.Instance.Player.Stats.HP, GameManager.Instance.TotalDamage);
            float newHP = GameManager.Instance.Player.Stats.HP;
            //승리 시 
            if (isVictory)
            {
                Utils.WriteColorLine(" Victory", ConsoleColor.Green);
                Console.WriteLine($"\n\n던전에서 몬스터 {GameManager.Instance.Monsters.Count} 마리를 잡았습니다.");
            }

            //패배 시
            else
            {
                Utils.WriteColorLine(" You Lose...", ConsoleColor.DarkRed);
            }


            Console.WriteLine($"\n\n Lv.{GameManager.Instance.Player.Stats.Level} {GameManager.Instance.Player.Stats.Name}");
            Console.WriteLine($" HP{oldHP} -> {newHP}");

            //데이터 초기화
            GameManager.Instance.TotalDamage = 0f;
            GameManager.Instance.Monsters.Clear();
        }
    }
}
