namespace SIX_Text_RPG
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
        Weapon,
        Count
    };

    public struct ItemInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public ConsoleColor Color { get; set; }

        public char Graphic { get; set; }

        public float HP { get; set; }
        public float MaxHP { get; set; }
        public float MP { get; set; }
        public float MaxMP { get; set; }

        public int ATK { get; set; }
        public int DEF { get; set; }
        public int Price { get; set; }

        public bool IsSold { get; set; }
        public bool IsEquip { get; set; }
    }


    internal abstract class Item
    {
        public ItemInfo Iteminfo { get; private set; }
        public ItemType Type { get; protected set; }

        public Item(ItemInfo iteminfo)
        {
            Iteminfo = iteminfo;
        }

        public void Sale()
        {
            SetBool(ItemBool.IsSold);
        }

        public void SetBool(ItemBool itemstat)
        {
            ItemInfo temp = Iteminfo;
            if (ItemBool.IsSold == itemstat)
            {
                temp.IsSold = !Iteminfo.IsSold;
            }
            else if (ItemBool.IsEquip == itemstat)
            {
                temp.IsEquip = !Iteminfo.IsEquip;
            }
            Iteminfo = temp;
        }
    }
}