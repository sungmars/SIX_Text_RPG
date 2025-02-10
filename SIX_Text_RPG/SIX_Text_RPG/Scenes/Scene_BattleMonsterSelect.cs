using System.Text;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleMonsterSelect : Scene_BattleScene
    {

        private Random random = new Random();
        private List<Monster> monsters = GameManager.Instance.Monsters;
        private Player? player = GameManager.Instance.Player;

        private int left = 0;
        private int top = 0;


        public override void Awake()
        {
            base.Awake();
            /* hasZero = true;

             for (int i = 0; i < monsters.Count; i++)
             {
                 Menu.Add(string.Empty);
             }

             zeroText = "취소";*/

            hasZero = false;

            for (int i = 0; i < monsters.Count; i++)
            {
                Utils.CursorMenu.Add((string.Empty, () => selectMonsterNum = i));
            }
            Utils.CursorMenu.Add(("돌아가기", () => Program.CurrentScene = new Scene_BattleLobby()));

            isPlayAnim = true;

        }
        public override void LateStart()
        {
            base.LateStart();
        }
        public override int Update()
        {
            /*int selectNum = base.Update();
            switch (selectNum)
            {
                case 0:
                    Program.CurrentScene = new Scene_BattleLobby();
                    return 0;
                default:
                    selectMonsterNum = selectNum - 1;
                    Program.CurrentScene = new Scene_BattlePhase();
                    return 0;
            }*/

            Utils.DisplayCursorMenu(79, 14);

            base.Update();

            if (Program.CurrentScene == new Scene_BattleLobby())
            {
                return 0;
            }
            else if (selectMonsterNum >= 0)
            {
                Program.CurrentScene = new Scene_BattlePhase();
                return 0;
            }
            return 0;

        }
    }
}