namespace SIX_Text_RPG
{
    internal class Accessory : Item, IEquipable
    {
        public Accessory(ItemInfo iteminfo, char graphic) : base(iteminfo)
        {
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