namespace SIX_Text_RPG
{
    internal class GameManager
    {
        public static GameManager Instance { get; private set; } = new();

        public Player? Player { get; set; }
        public List<Monster> Monsters { get; set; } = new();
        public float TotalDamage { get; set; } = 0;

        public int DisplayGround()
        {
            int width = Console.WindowWidth;
            int x = 0;
            int y = Console.CursorTop;
            Console.SetCursorPosition(x, y + 1);

            while (x < width)
            {
                Console.Write("〓");
                x += 2;
            }

            Console.WriteLine("\n");
            Console.WriteLine("(´◎ω◎)");
            Console.WriteLine("\n");

            x = 0;
            while (x < width)
            {
                Console.Write("〓");
                x += 2;
            }

            Console.ReadKey();
            return 0;
        }

        public void DisplayAttack(int startX, int startY, int endX, char sign)
        {
            (int left, int top) = Console.GetCursorPosition();

            while (startX < endX)
            {
                Console.Write(sign);
                Thread.Sleep(20);
                Console.SetCursorPosition(--startX, startY);
                Console.Write(' ');

                startX += 2;
            }

            Console.SetCursorPosition(left, top);
        }
    }
}