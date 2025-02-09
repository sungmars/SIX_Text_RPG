using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Scene_LevelUp : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 0:
                    Program.CurrentScene = new Scene_Lobby();
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Utils.WriteName(" name의 시간과 정신의 방");
            Console.ResetColor();
        }
    }
}