namespace SIX_Text_RPG
{
    internal class GameManager
    {
        public static GameManager Instance { get; private set; } = new();

        public Player? Player { get; set; }
    }
}