namespace SIX_Text_RPG
{
    public enum Type
    {
        Accessary,
        Armor,
        Bullet,
        Potion
    }

    public enum Effect
    {
        ATK,
        DEF,
        HP
    }

    public struct itemInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsSold { get; set; }
        public int Price { get; set; }
    }



    internal class Item
    {
        //public 
        //public Item(string name) 
    }
}