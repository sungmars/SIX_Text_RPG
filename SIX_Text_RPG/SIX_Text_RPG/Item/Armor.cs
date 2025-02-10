namespace SIX_Text_RPG
{
    internal class Armor : Item, IEquipable
    {
        public Armor(string name, string desciption, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price) : base(name, desciption, hp, maxhp, mp, maxmp, atk, def, price)
        {
        }

        public void Equip()
        {
            SetBool(ItemBool.IsEquip);
        }
    }
}