namespace SIX_Text_RPG
{
    internal class GameManager
    {
        public static GameManager Instance { get; private set; } = new();

        public Player? Player { get; set; }

        public List<Monster> Monsters { get; set; } = new();

        public float TotalDamage { get; set; } = 0;
    }
}