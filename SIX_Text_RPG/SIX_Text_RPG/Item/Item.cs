using System.Diagnostics;
using System.Numerics;

﻿namespace SIX_Text_RPG
{
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
    }



    internal abstract class Item
    {
        public ItemInfo Iteminfo { get; private set; }

        public Item(string name, string desciption, float hp, float maxhp, float mp, float maxmp, int atk, int def,int price)
        {
            Iteminfo = new ItemInfo()
            {
                Name = name,
                Description = desciption,
                HP = hp,
                MaxHP = maxhp,
                MP = mp,
                MaxMP = maxmp,
                ATK = atk,
                DEF = def,
                Price = price,
                IsSold = false
            };
        }
    }
}