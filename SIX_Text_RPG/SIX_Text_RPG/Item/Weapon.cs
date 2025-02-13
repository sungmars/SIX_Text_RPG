﻿namespace SIX_Text_RPG
{
    internal class Weapon : Item, IEquipable
    {

        public Weapon(ItemInfo iteminfo) : base(iteminfo)
        {
            this.Type = ItemType.Weapon;
        }

        public void Equip()
        {
            SetBool(ItemBool.IsEquip);

            if (GameManager.Instance.Player == null) return;

            if (Iteminfo.IsEquip == true) GameManager.Instance.Player.Equip(this);
            else GameManager.Instance.Player.Unequip(this);
        }
    }
}