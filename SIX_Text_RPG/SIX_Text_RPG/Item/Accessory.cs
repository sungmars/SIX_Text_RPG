namespace SIX_Text_RPG
{
    internal class Accessory : Item, IEquipable, IGraphicable
    {
        public Char Graphic { get; set; }

        public Accessory(ItemInfo iteminfo, char graphic) : base(iteminfo)
        {
            this.Graphic = graphic;
            this.Type = ItemType.Accessory;
        }

        public void Equip()
        {
            if (GameManager.Instance.Player == null) return;

            SetBool(ItemBool.IsEquip);
            
            if (Iteminfo.IsEquip == true) GameManager.Instance.Player.Equip(this);
            else GameManager.Instance.Player.Unequip(this);
        }
    }
}