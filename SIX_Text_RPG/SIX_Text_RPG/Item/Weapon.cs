namespace SIX_Text_RPG
{
    internal class Weapon : Item, IEquipable, IGraphicable
    {
        public char Graphic { get; set; }

        public Weapon(ItemInfo iteminfo, char graphic) : base(iteminfo)
        {
            this.Graphic = graphic;
            this.Type = ItemType.Accessory;
        }

        public void Equip()
        {
            SetBool(ItemBool.IsEquip);

            if (GameManager.Instance.Player == null) return;

            if (Iteminfo.IsEquip == true) GameManager.Instance.Player.Equip(this);
            else GameManager.Instance.Player.Unequip(this);
        }
    }
}