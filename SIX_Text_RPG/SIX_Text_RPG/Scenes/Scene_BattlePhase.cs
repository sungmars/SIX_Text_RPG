using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattlePhase : Scene_BattleScene
    {



        private int left = 0;
        private int top = 0;


        public override void Awake()
        {
            base.Awake();
            hasZero = false;
            Utils.CursorMenu.Add(("player다음", () => MonsterNext()));
            isPlayAnim = true;
        }

        public override int Update()
        {
            if (PlayerPhase())
            {
                Program.CurrentScene = new Scene_BattleResult(true);
                return 0;
            }
            else
            {
                Utils.DisplayCursorMenu(5, CURSORMENU_TOP);
                base.Update();
                return 0;
            }
        }

        public override void LateStart()
        {
            base.LateStart();
        }

        protected override void Display()
        {
            base.Display();
        }

        private void MonsterNext()
        {
            if (MonsterPhase())
            {
                // 플레이어 사망시 결과로
                Program.CurrentScene = new Scene_BattleResult(false);
            }
            else
            {
                Utils.CursorMenu.Clear();
                Utils.CursorMenu.Add(("monster다음", () => Program.CurrentScene = new Scene_BattleLobby()));
                Utils.DisplayCursorMenu(5, CURSORMENU_TOP);
                base.Update();
            }
        }



    }
}
