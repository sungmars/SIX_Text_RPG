using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleMonsterSelect : Scene_BattleScene
    {

        private Random random = new Random();
        private List<Monster> monsters = GameManager.Instance.Monsters;
        private Player? player = GameManager.Instance.Player;

        private int selectMonsterNum;

        private int left = 0;
        private int top = 0;


        public override void Awake()
        {

            hasZero = false;


            for (int i = 0; i < monsters.Count; i++)
            {
                int index = i;
                Utils.CursorMenu.Add((string.Empty, () => selectMonsterNum = index));
            }
            Utils.CursorMenu.Add(("찌르기 취소", () => selectMonsterNum = monsters.Count));


        }
        public override void LateStart()
        {
        }
        public override int Update()
        {

            Utils.DisplayCursorMenu(78, 16);
            if (base.Update() == 0)
            {
                for(int i = 0; i <= monsters.Count; i++)
                {
                    Utils.ClearLine(74, 15 + i);
                }
                Utils.CursorMenu.Clear();
                GameManager.Instance.DisplayBattle();
            }

            if (monsters.Count == selectMonsterNum)
            {
                Program.CurrentScene = new Scene_BattleLobby();
                return 0;
            }
            else
            {
                PlayerPhase(selectMonsterNum);
                Program.CurrentScene = new Scene_BattlePhase();
                return 0;
            }
            return 0;

        }

        

    }
}