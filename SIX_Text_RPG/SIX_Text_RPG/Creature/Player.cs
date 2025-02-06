namespace SIX_Text_RPG
{
    public enum PlayerType
    {
        None
    }

    internal class Player : Creature
    {
        public PlayerType Type { get; private set; }

        public void SetGold(int gold)
        {
            Stats stats = Stats;
            stats.Gold += gold;
            Stats = stats;
        }
    }
}