using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Program
    {
        public static Scene_Base CurrentScene { get; set; } = new Scene_Title();
        public static Scene_Base PreviousScene { get; set; } = CurrentScene;

        static void Main()
        {
            Console.CursorVisible = false;
            CurrentScene = new Scene_Title();

            while (CurrentScene != null)
            {
                CurrentScene.Awake();
                CurrentScene.Start();
                CurrentScene.LateStart();

                do if (CurrentScene.Update() == 0) break;
                while (true);
            }
        }
    }
}