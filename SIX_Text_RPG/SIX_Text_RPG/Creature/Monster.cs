namespace SIX_Text_RPG
{
    public enum MonsterType
    {
        None
    }

    internal class Monster : Creature
    {
        public MonsterType Type { get; private set; }
    }
}