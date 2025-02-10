using System.Diagnostics;
using System.Numerics;

﻿namespace SIX_Text_RPG
{
    enum ItemStat
    {
        Name,
        Description,
        HP,
        MaxHP,
        MP,
        MaxMP,
        ATK,
        DEF,
        Price,
        IsSold,
        IsEquip
    }

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
                IsSold = false,
                IsEquip = false
            };
        }

        //아이템의 bool타입 정보 수정하는 것
        protected void SetBool(ItemStat itemstat)
        {
            ItemInfo temp = Iteminfo;
            if (ItemStat.IsSold == itemstat)
            {
                temp.IsSold = true;
                Iteminfo = temp;
            }
            else if (ItemStat.IsEquip == itemstat)
            {
                temp.IsEquip = true;
                Iteminfo = temp;
            }
        }

        //아이템의 string타입 정보 가져오기
        public string GetStatString(ItemStat itemstat)
        {
            string value = "";
            switch (itemstat)
            {
                case ItemStat.Name:
                    value = Iteminfo.Name;
                    break;

                case ItemStat.Description:
                    value = Iteminfo.Description;
                    break;
            }
            return value;

        }

        //아이템의 int타입 정보 가져오기
        public int GetStatInt(ItemStat itemstat)
        {
            int value = -1;
            switch (itemstat)
            {
                case ItemStat.DEF:
                    value = Iteminfo.DEF;
                    break;

                case ItemStat.ATK:
                    value = Iteminfo.ATK;
                    break;

                case ItemStat.Price:
                    value = Iteminfo.Price;
                    break;
                    
            }
            return value;
        }

        //아이템의 float타입 정보 가져오기
        public float GetStatFloat(ItemStat itemstat)
        {
            float value = -999f;
            switch (itemstat)
            {
                case ItemStat.MaxHP:
                    value = Iteminfo.MaxHP;
                    break;

                case ItemStat.HP:
                    value = Iteminfo.HP;
                    break;

                case ItemStat.MaxMP:
                    value = Iteminfo.MaxMP;
                    break;

                case ItemStat.MP:
                    value = Iteminfo.MP;
                    break;
            }
            return value;
        }

        //아이템의 bool타입 정보 가져오기
        public bool GetStatBool(ItemStat itemstat)
        {
            bool value = false;
            switch (itemstat)
            {
                case ItemStat.IsSold:
                    value = Iteminfo.IsSold;
                    break;

                case ItemStat.IsEquip:
                    value = Iteminfo.IsEquip;
                    break;
            }
            return value;
        }
    }
}