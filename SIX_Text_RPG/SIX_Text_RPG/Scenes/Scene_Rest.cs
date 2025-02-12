using SIX_Text_RPG.Managers;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Rest : Scene_Base
    {
        private readonly int cursorX = 50;
        private readonly int cursorY = 7;

        public override void Awake()
        {
            base.Awake();

            sceneTitle = "캠 끄기 (휴식)";
            sceneInfo = "화장실을 간다며 캠을 끄고 침대로 향합니다. 곧 기분이 좋아지며 춤을 춥니다.";
        }

        public override int Update()
        {
            switch (base.Update())
            {
                default:
                    Program.CurrentScene = new Scene_Lobby();
                    break;
            }

            return 0;
        }

        public override void LateStart()
        {
            base.LateStart();
            RenderManager.Instance.Play("Rest", cursorX, cursorY - 1);
        }

        protected override void Display()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            // 체력 회복 전
            Console.SetCursorPosition(1, cursorY + 4);
            player.DisplayInfo_Status();

            // 체력 회복 애니메이션
            player.StatusAnim(Stat.HP, 100);
            player.SetStat(Stat.HP, player.Stats.MaxHP);

            // 체력 회복 후 상태창 갱신
            Console.SetCursorPosition(1, cursorY + 4);
            player.DisplayInfo_Status();

            // 체력을 모두 회복했을 때
            Console.SetCursorPosition(1, Console.CursorTop);
            if (player.Stats.MaxHP == player.Stats.HP)
            {
                Console.SetCursorPosition(1, cursorY + 11);
                Utils.WriteColor("체력이 모두 회복되었습니다.", ConsoleColor.DarkCyan);
            }

            Console.SetCursorPosition(1, cursorY + 15);
        }
    }
}