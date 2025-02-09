﻿namespace SIX_Text_RPG
{
    internal class Armor : Item, IEquipable
    {
        public Armor(string name, string desciption, float hp, int price, int atk, int def) : base(name, desciption, hp, price, atk, def)
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