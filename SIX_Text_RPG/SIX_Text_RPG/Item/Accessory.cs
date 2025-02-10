namespace SIX_Text_RPG
{
    internal class Accessory : Item, IEquipable
    {
        public Char Graphic { get; private set; }

        public Accessory(ItemInfo iteminfo, char graphic) : base(iteminfo)
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