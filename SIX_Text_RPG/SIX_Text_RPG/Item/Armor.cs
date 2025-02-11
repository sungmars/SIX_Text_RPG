using SIX_Text_RPG.Scenes;

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

            if (GameManager.Instance.Player == null) return;
            if (Iteminfo.IsEquip == true) GameManager.Instance.Player.Equip(this);
            else GameManager.Instance.Player.Unequip(this);
        }
    }
}