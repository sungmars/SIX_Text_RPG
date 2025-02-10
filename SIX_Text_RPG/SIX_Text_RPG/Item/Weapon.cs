namespace SIX_Text_RPG
{
    internal class Weapon : Item, IEquipable
    {
        public char Graphic { get; private set; }

        public Weapon(ItemInfo iteminfo, char graphic) : base(iteminfo)
        {
            this.Graphic = graphic;
            this.Type = ItemType.Accessory;
        }



        public void Equip()
        {
            SetBool(ItemBool.IsEquip);
        }
    }
}