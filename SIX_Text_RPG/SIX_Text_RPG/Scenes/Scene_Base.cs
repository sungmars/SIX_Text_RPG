namespace SIX_Text_RPG
{
    internal abstract class Scene_Base
    {
        public int MenuCount { get { return Menu.Count; } }

        protected string sceneTitle = string.Empty;
        protected string sceneInfo = string.Empty;

        protected readonly List<string> Menu = new();

        public virtual void Awake()
        {
            Console.Clear();
        }

        public void Start()
        {
            Utils.WriteColorLine($"\n {sceneTitle}", ConsoleColor.DarkYellow);
            Utils.WriteColorLine($" {sceneInfo}\n", ConsoleColor.DarkGray);
            Display();
        }
        
        public void LateStart()
        {
            Display_Menu();
        }

        public virtual int Update()
        {
            return Utils.ReadIndex();
        }

        protected abstract void Display();

        private void Display_Menu()
        {
            Console.WriteLine();

            for (int i = 0; i < Menu.Count; i++)
            {
                Console.WriteLine($" [{i + 1}] {Menu[i]}");
            }

            Console.WriteLine("\n [0] 나가기\n");
        }
    }
}