namespace SIX_Text_RPG
{
    internal class Accessory : Item, IEquipable
    {
        public string Form {  get; set; }
        public Accessory(string name, string desciption, float hp, int price, int atk, int def, string form) : base(name, desciption, hp, price, atk, def)
        {
            this.Form = form;
        }

        void IEquipable.Equip()
        {
            if (GameManager.Instance.Player == null) return;

            Player player = GameManager.Instance.Player;
            //장착하는곳에서 장착 구현
        }
    }
}