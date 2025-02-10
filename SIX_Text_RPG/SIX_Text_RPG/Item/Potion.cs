namespace SIX_Text_RPG
{
    internal class Potion : Item, IConsumable
    {
        public Potion(string name, string desciption, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price) : base(name, desciption, hp, maxhp, mp, maxmp, atk, def, price)
        {
        }

        void IConsumable.Consume()
        {
            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            player.SetStat(Stat.MaxHP, Iteminfo.MaxHP);
            player.SetStat(Stat.HP, Iteminfo.HP);
            //인벤토리에 사라지게하는 것 구현필요
        }
    }
}