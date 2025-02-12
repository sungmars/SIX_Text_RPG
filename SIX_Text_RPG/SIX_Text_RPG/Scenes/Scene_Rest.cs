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

            sceneTitle = "ķ ���� (�޽�)";
            sceneInfo = "ȭ����� ���ٸ� ķ�� ���� ħ��� ���մϴ�. �� ����� �������� ���� ��ϴ�.";
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

            // ü�� ȸ�� ��
            Console.SetCursorPosition(1, cursorY + 4);
            player.DisplayInfo_Status();

            // ü�� ȸ�� �ִϸ��̼�
            player.StatusAnim(Stat.HP, 100);
            player.SetStat(Stat.HP, player.Stats.MaxHP);

            // ü�� ȸ�� �� ����â ����
            Console.SetCursorPosition(1, cursorY + 4);
            player.DisplayInfo_Status();

            // ü���� ��� ȸ������ ��
            Console.SetCursorPosition(1, Console.CursorTop);
            if (player.Stats.MaxHP == player.Stats.HP)
            {
                Console.SetCursorPosition(1, cursorY + 11);
                Utils.WriteColor("ü���� ��� ȸ���Ǿ����ϴ�.", ConsoleColor.DarkCyan);
            }

            Console.SetCursorPosition(1, cursorY + 15);
        }
    }
}