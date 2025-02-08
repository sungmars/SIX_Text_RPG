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
            Console.Clear();
        }

        public void LateAwake()
        {
            Utils.WriteColorLine($"\n {sceneTitle}", ConsoleColor.DarkYellow);
            if (sceneTitle == string.Empty)
            {
                return;
            }

            Utils.DisplayLine();
        }

        public void Start()
        {
            Utils.WriteColor($" {sceneInfo}", ConsoleColor.DarkGray);
            if (sceneInfo != string.Empty)
            {
                Utils.DisplayLine();
            }

            Console.WriteLine();
            Display();
        }

        public void LateStart()
        {
            if (Menu.Count == 0 && !hasZero)
            {
                return;
            }

            Display_Menu();
            Utils.DisplayLine();
        }

        public virtual int Update()
        {
            return Utils.ReadIndex(hasZero);
        }

        protected abstract void Display();

        private void Display_Menu()
        {
            Console.WriteLine();

            for (int i = 0; i < Menu.Count; i++)
            {
                if (Menu[i] == string.Empty)
                {
                    continue;
                }

                Utils.WriteMenuLine($" [{i + 1}] {Menu[i]}");
            }

            if (hasZero)
            {
                Console.WriteLine($"\n [0] {zeroText}");
            }
        }
    }
}