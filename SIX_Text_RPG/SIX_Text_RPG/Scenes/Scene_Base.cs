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
            Utils.DisplayLine(!Program.IsSameScene);
            Program.PreviousScene = this;
        }

        public virtual int Update()
        {
            if (Utils.CursorMenu.Count > 0) return Utils.ReadArrow();
            else return Utils.ReadIndex(hasZero);
        }

        protected abstract void Display();

        private void Display_Menu()
        {
            Console.WriteLine();
            int delay = Program.IsSameScene ? 0 : 200;

            for (int i = 0; i < Menu.Count; i++)
            {
                if (Menu[i] == string.Empty)
                {
                    continue;
                }

                string[] texts = Menu[i].Split('\n');
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
                Console.WriteLine($"\n [0] {zeroText}");
                Thread.Sleep(delay);
            }
        }
    }
}