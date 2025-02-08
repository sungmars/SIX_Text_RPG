namespace SIX_Text_RPG
{
    public struct Vector2
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    internal class GameManager
    {
        public static GameManager Instance { get; private set; } = new();

        public Player? Player { get; set; }
        public List<Monster> Monsters { get; private set; } = new();
        public float TotalDamage { get; set; } = 0;

        public void DisplayBattle(int targetIndex, int attackCount = 1)
        {
            if (Player == null)
            {
                return;
            }

            // TEST CODE
            Monsters.Add(new(MonsterType.염예찬));
            Monsters.Add(new(MonsterType.강성훈));
            Monsters.Add(new(MonsterType.강인));
            Monsters.Add(new(MonsterType.권관우));

            DisplayBattle_Ground();
            (int left, int top) = Console.GetCursorPosition();

            DisplayBatte_Monsters();
            DisplayBattle_Attack(targetIndex, Player.Graphic_Weapon, attackCount);

            Console.SetCursorPosition(left, top);
            Utils.ClearBuffer();

            // TEST CODE
            Console.ReadKey();
        }

        private void DisplayBattle_Attack(int targetIndex, char sign, int attackCount)
        {
            if (Player == null)
            {
                return;
            }

            int[] startX = new int[attackCount];
            int fireX = targetIndex <= 1 ? Player.Stats.Name.Length * 2 : Player.Position.X;
            int endX = Define.MONSTER_SPAWN_X - 2;
            int targetY = Monsters[targetIndex].Position.Y;
            string value = sign.ToString();

            for (int i = 0; i < attackCount; i++)
            {
                if (targetIndex == 1)
                {
                    startX[i] = fireX - i * 6;
                    continue;
                }

                startX[i] = Player.Position.X - i * 6;
            }

            while (startX[^1] < endX)
            {
                for (int i = 0; i < attackCount; i++)
                {
                    if (startX[i]++ < fireX || startX[i] > endX)
                    {
                        continue;
                    }

                    Console.SetCursorPosition(startX[i]--, targetY);
                    Utils.WriteColor(value, ConsoleColor.Yellow);
                }

                Thread.Sleep(5);

                for (int i = 0; i < attackCount; i++)
                {
                    if (startX[i]++ < fireX || startX[i] > endX)
                    {
                        continue;
                    }

                    if (startX[i] == endX)
                    {
                        Monsters[targetIndex].Render_Hit();
                    }

                    Console.SetCursorPosition(startX[i], targetY);
                    Console.Write(' ');
                }
            }
        }

        private void DisplayBattle_Ground()
        {
            if (Player == null)
            {
                return;
            }

            DisplayBattle_Line();

            Utils.WriteName("\n name");
            Console.Write(" (´◎ω◎)");
            Player.SetPosition(Console.CursorLeft, Console.CursorTop);

            DisplayBattle_Line();
        }

        private void DisplayBattle_Line()
        {
            int x = 0;
            int y = Console.CursorTop;
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine();

            int width = Console.WindowWidth;
            while (x < width)
            {
                Console.Write("〓");
                x += 2;
            }
            Console.WriteLine();
        }

        private void DisplayBatte_Monsters()
        {
            int top = Console.CursorTop - 5;
            for (int i = 0; i < Monsters.Count; i++)
            {
                Monsters[i].SetPosition(Monsters[i].Position.X, top);
                Console.SetCursorPosition(Monsters[i].Position.X, top++);
                Monsters[i].Render();
                Utils.WriteColor(Monsters[i].Stats.Name, ConsoleColor.DarkCyan);
                Monsters[i].Display_HealthBar();
            }
        }
    }
}