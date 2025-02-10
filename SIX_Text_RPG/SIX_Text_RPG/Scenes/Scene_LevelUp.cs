using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Scene_LevelUp : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();

            sceneTitle = "축하합니다! 레벨이 증가했습니다!";
            Menu.Add("레벨");
            Menu.Add("경험치");
            Menu.Add("공격력");
            Menu.Add("방어력");
            Menu.Add("골드");
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    GameManager.Instance.Player.StatusAnim(Stat.Level, 3);
                    break;
                case 2:
                    GameManager.Instance.Player.StatusAnim(Stat.EXP, 33);
                    break;
                case 3:
                    GameManager.Instance.Player.StatusAnim(Stat.ATK, 33);
                    break;
                case 4:
                    GameManager.Instance.Player.StatusAnim(Stat.DEF, 33);
                    break;
                case 5:
                    GameManager.Instance.Player.StatusAnim(Stat.Gold, 33);
                    break;
                case 0:
                    Program.CurrentScene = new Scene_Lobby();
                    break;
            }

            Console.ReadKey();
            return 0;
        }

        protected override void Display()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            player.DisplayInfo();
        }
    }
}