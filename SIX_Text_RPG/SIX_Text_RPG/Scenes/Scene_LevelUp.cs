using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Scene_LevelUp : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();
            Menu.Add("테스트");
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    break;
                case 0:
                    Program.CurrentScene = new Scene_Lobby();
                    return 0;
            }

            return 1;
        }

        protected override void Display()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Utils.WriteName(" name의 시간과 정신의 방");
            Console.ResetColor();
        }
    }
}