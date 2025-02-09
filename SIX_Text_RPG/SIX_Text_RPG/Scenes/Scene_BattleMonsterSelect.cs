using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleMonsterSelect : Scene_BattleDisplay
    {

        private Random random = new Random();
        private List<Monster> monsters = GameManager.Instance.Monsters;
        private Player? player = GameManager.Instance.Player;
        private bool isSelectPhase = true;

        private int left = 0;
        private int top = 0;


        public override void Awake()
        {
            base.Awake();
            isSelectMonster = true;
            hasZero = true;
            if (isSelectPhase == true)//몬스터 선택 페이즈
            {
                for (int i = 0; i < monsters.Count; i++)
                {
                    Menu.Add(string.Empty);
                }

                zeroText = "취소";
            }
        }

        public override int Update()
        {
            int selectNum = base.Update();
            switch(selectNum)
            {
                case 0:
                    Program.CurrentScene = new Scene_BattleLobby();
                    return 0;
                default:
                    Program.CurrentScene = new Scene_BattlePhase(selectNum-1);
                    return 0;
            }
        }

        protected override void Display()
        {
            base.Display();
        }

    }
}
