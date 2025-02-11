using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Managers
{
    internal class ItemManager
    {
        public static ItemManager Instance { get; private set; } = new();

        public List<List<Item>> StoreItem = new List<List<Item>>();

        public ItemManager()
        {
            StoreItem.Add(new List<Item>());
        }
        public bool Istalk_1 { get; private set; } = false;
        public bool Istalk_2 { get; private set; } = false;
        public bool Istalk_3 { get; private set; } = false;


        public void SetBool(int num, bool value)
        {
            if (num == 1)
            {
                Istalk_1 = value;
            }
            else if (num == 2)
            {
                Istalk_2 = value;
            }
            else if (num == 3)
            {
                Istalk_3 = value;
            }
        }
    }
}
