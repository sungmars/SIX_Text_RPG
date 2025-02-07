﻿namespace SIX_Text_RPG
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

                Console.WriteLine($" [{i + 1}] {Menu[i]}");
            }

            if (hasZero)
            {
                Console.WriteLine($"\n [0] {zeroText}\n");
            }
        }
    }
}