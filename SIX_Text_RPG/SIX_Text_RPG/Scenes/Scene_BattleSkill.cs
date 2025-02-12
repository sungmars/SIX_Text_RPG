namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleSkill : Scene_Battle
    {
        private readonly int CURSOR_MENU_X = 30;
        private readonly int CURSOR_MENU_Y = 23;

        public override void Awake()
        {
            hasZero = false;

            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            // 스킬 메뉴 생성
            var skills = player.Skills.Where(skill => skill.Mana > 0).ToList();
            for (int i = 0; i < skills.Count; i++)
            {
                var selectedSkill = skills[i];
                Utils.CursorMenu.Add((selectedSkill.Name, () =>
                {
                    int index = i;
                    UseSkill(index);
                    Program.CurrentScene = new Scene_BattleInventory();
                }
                ));
            }

            // 가방 닫기시 메뉴 다 지우고 로비로 돌아가기
            Utils.CursorMenu.Add(("가방 닫기", () =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Utils.ClearLine(0, CURSOR_MENU_Y + i);
                }
                Utils.CursorMenu.Clear();
                Program.CurrentScene = new Scene_BattleLobby();
            }
            ));
        }

        public override void LateStart()
        {
            Utils.DisplayCursorMenu(CURSOR_MENU_X, CURSOR_MENU_Y);
        }

        public void UseSkill(int index)
        {

        }
    }
}