using System.Numerics;

namespace SIX_Text_RPG.Item
{
    public struct ItemInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public float MaxHP { get; set; }
        public float HP { get; set; }

        public int Price { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }

        public bool IsSold { get; set; }
    }



    internal abstract class Item
    {
        public ItemInfo Iteminfo { get; private set; }

        public Item(string name, string desciption, float hp, int price, int atk, int def)
        {
            Iteminfo = new ItemInfo()
            {
                Name = name,
                Description = desciption,
                HP = hp,
                Price = price,
                ATK = atk,
                DEF = def,
                IsSold = false
            };
        }
    }
}