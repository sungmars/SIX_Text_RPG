namespace SIX_Text_RPG
{
    internal class Accessory : Item, IEquipable
    {
        public Char Graphic { get; set; }
        public Accessory(string name, string desciption, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price, char graphic) : base(name, desciption, hp, maxhp, mp, maxmp, atk, def, price)
        {
            this.Graphic = graphic;
        }

        void IEquipable.Equip()
        {
            if (GameManager.Instance.Player == null) return;

            Player player = GameManager.Instance.Player;
            //장착 매서드 나오게 되면 사용하기
        }
    }
}