using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Armor : Item, IEquipable
    {
        public Armor(string name, string desciption, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price) : base(name, desciption, hp, maxhp, mp, maxmp, atk, def, price)
        {
            type = ItemType.Armor;
        }

        public void Equip()
        {
            GetStatBool(ItemStat.IsEquip);
        }
    }
}