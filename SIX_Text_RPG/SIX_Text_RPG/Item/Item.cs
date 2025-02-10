using System.Diagnostics;
using System.Numerics;

﻿namespace SIX_Text_RPG
{
    enum ItemBool
    {
        IsSold,
        IsEquip
    }

    public enum ItemType
    {
        Armor,
        Accessory,
        Potion,
        Weapon
    };

    public struct ItemInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public float HP { get; set; }
        public float MaxHP { get; set; }
        public float MP { get; set; }
        public float MaxMP { get; set; }

        public int ATK { get; set; }
        public int DEF { get; set; }
        public int Price { get; set; }

        public bool IsSold { get; set; }
        public bool IsEquip {  get; set; }
    }


    internal abstract class Item
    {
        public ItemInfo Iteminfo { get; private set; }
        public ItemType Type { get; protected set; }

        public Item(ItemInfo iteminfo)
        {
            Iteminfo = iteminfo;
        }


        //아이템의 bool타입 정보 수정하는 것
        public void SetBool(ItemBool itemstat)
        {
            ItemInfo temp = Iteminfo;
            if (ItemBool.IsSold == itemstat)
            {
                temp.IsSold = !Iteminfo.IsSold;
            }
            else if (ItemBool.IsEquip == itemstat)
            {
                temp.IsEquip = Iteminfo.IsEquip;
            }
            Iteminfo = temp;
        }
    }
}