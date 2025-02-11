using System.Xml;

namespace SIX_Text_RPG
{
    internal class Potion : Item, IConsumable
    {
        public Potion(ItemInfo iteminfo) : base(iteminfo)
        {
            this.Type = ItemType.Potion;
        }

        bool IConsumable.Consume()
        {
            if (GameManager.Instance.Player == null) return false;
            Player player = GameManager.Instance.Player;
            
            //포션 능력치에 맞게 플레이어 스텟 변화
            player.SetStat(Stat.MaxHP, Iteminfo.MaxHP, true);
            player.SetStat(Stat.HP, Iteminfo.HP, true);
            player.SetStat(Stat.MaxMP, Iteminfo.MaxMP, true);
            player.SetStat(Stat.MP, Iteminfo.MP, true);
            //인벤토리에 사라지게하는 것
            return true;
        }
    }
}