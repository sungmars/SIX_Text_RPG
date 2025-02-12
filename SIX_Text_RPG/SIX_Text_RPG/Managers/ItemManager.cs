namespace SIX_Text_RPG
{
    internal class ItemManager
    {
        public static ItemManager Instance { get; private set; } = new();

        public List<List<Item>> StoreItem = new List<List<Item>>();

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
            else if (num == 3)
            {
                Istalk_3 = value;
            }
        }

        public static ItemManager Instance { get; private set; } = new();

        public readonly List<Item>[] StoreItems = new List<Item>[(int)ItemType.Count];
    }
}