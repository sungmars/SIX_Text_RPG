namespace SIX_Text_RPG
{
    internal class Armor : Item, IEquipable
    {
        public Armor(string name, string desciption, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price) : base(name, desciption, hp, maxhp, mp, maxmp, atk, def, price)
        {
        }

        void IEquipable.Equip()
        {
            if (GameManager.Instance.Player == null) return;

            Player player = GameManager.Instance.Player;

            player.SetStat(Stat.DEF, Iteminfo.DEF, true);
            player.SetStat(Stat.ATK, Iteminfo.ATK, true);
            player.SetStat(Stat.HP, Iteminfo.HP, true);
            player.SetStat(Stat.MaxHP, Iteminfo.HP, true);
        }
    }
}