using SIX_Text_RPG.Managers;

namespace SIX_Text_RPG
{
    internal abstract class Scene_Base
    {
        public int MenuCount { get { return Menu.Count; } }

        protected bool hasZero = true;
        protected string sceneTitle = string.Empty;
        protected string sceneInfo = string.Empty;
        protected string zeroText = "나가기";

        protected readonly List<string> Menu = new();

        public virtual void Awake()
        {
            Menu.Clear();

            RenderManager.Instance.Stop();
            Console.Clear();
            Utils.CursorMenu.Clear();
        }

        public void Start()
        {
            if (sceneTitle != string.Empty)
            {
                Utils.WriteColorLine($"\n {sceneTitle}", ConsoleColor.DarkYellow);
                Utils.DisplayLine();
            }

            if (sceneInfo != string.Empty)
            {
                Utils.WriteColor($" {sceneInfo}", ConsoleColor.DarkGray);
                Utils.DisplayLine();
            }

            Console.WriteLine();
            Display();
        }

        public virtual void LateStart()
        {
            if (Utils.CursorMenu.Count > 0)
            {
                return;
            }

            if (Menu.Count == 0 && !hasZero)
            {
                return;
            }

            Display_Menu();
            Utils.DisplayLine(Program.CurrentScene != Program.PreviousScene);
            Program.PreviousScene = Program.CurrentScene;
        }

        public virtual int Update()
        {
            if (Utils.CursorMenu.Count > 0) return Utils.ReadArrow();
            else return Utils.ReadIndex(hasZero);
        }

        protected abstract void Display();

        private void Display_Menu()
        {
            int delay = Program.CurrentScene == Program.PreviousScene ? 0 : 200;
            Console.WriteLine();

            for (int i = 0; i < Menu.Count; i++)
            {
                if (Menu[i] == string.Empty)
                {
                    continue;
                }

                string[] texts = Menu[i].Split('\n');
                AudioManager.Instance.Play(AudioClip.SoundFX_DrawMenu);
                Console.WriteLine($" [{i + 1}] {texts[0]}");
                Thread.Sleep(delay);

                for (int j = 1; j < texts.Length; j++)
                {
                    Console.Write("     ");
                    Utils.WriteAnim(texts[j], ConsoleColor.DarkGray);
                }
            }

            if (hasZero)
            {
                AudioManager.Instance.Play(AudioClip.SoundFX_DrawMenu);
                Console.WriteLine($"\n [0] {zeroText}");
                Thread.Sleep(delay);
            }
        }
    }
}