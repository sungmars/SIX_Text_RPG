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



        public override void Awake()
        {
            hasZero = false;
            Utils.CursorMenu.Add(("player다음", () => MonsterNext()));
        }

        public override int Update()
        {

            if (IsAllMonsterDead())
            {
                Program.CurrentScene = new Scene_BattleResult(true);
                return 0;
            }
            else
            {
                Utils.DisplayCursorMenu(5, CURSORMENU_TOP);
                if(base.Update() == 0)
                {
                    Utils.CursorMenu.Clear();
                    Utils.ClearLine(0, CURSORMENU_TOP);
                    return 0;
                }
                return 0;
            }
        }

        public override void LateStart()
        {
            Console.Write(string.Empty);
        }

        protected override void Display()
        {
            base.Display();
        }

        private void MonsterNext()
        {
            if (MonsterPhase())
            {
                Program.CurrentScene = new Scene_BattleResult(false);
            }
            else
            {
                Utils.CursorMenu.Clear();
                Utils.CursorMenu.Add(("monster다음", () => Program.CurrentScene = new Scene_BattleLobby()));
                Utils.DisplayCursorMenu(5, CURSORMENU_TOP);
                if (base.Update() == 0)
                {
                    Utils.CursorMenu.Clear();
                    Utils.ClearLine(0, CURSORMENU_TOP);
                }
            }
        }

        private bool IsAllMonsterDead()
        {
            return monsters.All(monster => monster.IsDead);
        }

    }
}
