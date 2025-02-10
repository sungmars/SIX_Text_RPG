namespace SIX_Text_RPG
{
    internal class Armor : Item, IEquipable
    {
        public Armor(ItemInfo iteminfo) : base(iteminfo)
        {
            this.Type = ItemType.Armor;
        }

        public void Equip()
        {
            SetBool(ItemBool.IsEquip);
        }
    }
}