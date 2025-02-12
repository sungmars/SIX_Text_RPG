namespace SIX_Text_RPG
{
    internal class ItemManager
    {
        public ItemManager()
        {
            ItemInfo[,] iteminfo = Define.ITEM_INFOS;
            for (int i = 0; i < (int)ItemType.Count; i++)
            {
                StoreItems[i] = new();
                for (int j = 0; j < iteminfo.GetLength(1); j++)
                {
                    switch (i)
                    {
                        case (int)ItemType.Armor:
                            StoreItems[i].Add(new Armor(iteminfo[i, j]));
                            break;

                        case (int)ItemType.Accessory:
                            StoreItems[i].Add(new Accessory(iteminfo[i, j]));
                            break;

                        case (int)ItemType.Potion:
                            StoreItems[i].Add(new Potion(iteminfo[i, j]));
                            break;

                        case (int)ItemType.Weapon:
                            StoreItems[i].Add(new Weapon(iteminfo[i, j]));
                            break;
                    }
                }
            }
        }

        public static ItemManager Instance { get; private set; } = new();

        public readonly List<Item>[] StoreItems = new List<Item>[(int)ItemType.Count];

        public void SetBool_StoreItem(ItemType type, Item item)
        {
            var storeItems = StoreItems[(int)type];
            var storItem = storeItems.Find(x => x.Iteminfo.Name.Equals(item.Iteminfo.Name));
            storItem?.SetBool(ItemBool.IsSold);
        }
    }
}