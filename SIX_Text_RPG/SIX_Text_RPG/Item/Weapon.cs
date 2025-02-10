namespace SIX_Text_RPG
{
    internal class Weapon : Item, IEquipable
    {
        public char Graphic { get; set; }
        
        public Weapon(string name, string desciption, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price, char graphic) : base(name, desciption, hp, maxhp, mp, maxmp, atk, def, price)
        {
            Graphic = graphic;
        }

        public void Equip()
        {
            GetStatBool(ItemStat.IsEquip);
        }
    }
}