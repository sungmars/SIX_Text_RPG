namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleSelect : Scene_Battle
    {
        private readonly int CURSOR_MENU_X = 78;
        private readonly int CURSOR_MENU_Y = 16;
        private readonly int EXIT_OFFSET_Y = 7;

        public override void Awake()
        {
            hasZero = false;

            for (int i = 0; i < monsters.Count; i++)
            {
                int index = i;
                Utils.CursorMenu.Add((string.Empty, () => monsterIndex = index));
            }

            Utils.CursorMenu.Add(("찌르지 말고 조금만 더 기다려볼까?", () => monsterIndex = monsters.Count));
        }

        public override void LateStart()
        {
            Utils.DisplayCursorMenu(CURSOR_MENU_X, CURSOR_MENU_Y, EXIT_OFFSET_Y);
        }

        public override int Update()
        {
            if (base.Update() == 0)
            {
                for (int i = 0; i <= monsters.Count; i++)
                {
                    Utils.ClearLine(CURSOR_MENU_X - 3, CURSOR_MENU_Y + i - 1, 3);
                }

                Utils.ClearLine(CURSOR_MENU_X - 3, CURSOR_MENU_Y + EXIT_OFFSET_Y);
                Utils.CursorMenu.Clear();

                if (monsters.Count == monsterIndex)
                {
                    Program.CurrentScene = new Scene_BattleLobby();
                }
                else
                {
                    Player? player = GameManager.Instance.Player;
                    if (player == null)
                    {
                        return -1;
                    }

                    // 크리티컬 발동 시 두배 공격
                    var critical = player.Skills.Find(x => x.GetType() == typeof(Skill_Critical));
                    if (critical != null && critical.Skill())
                    {
                        attackCount *= 2;
                    }

                    Program.CurrentScene = new Scene_BattlePhase();
                }

                return 0;
            }

            return -1;
        }
    }
}