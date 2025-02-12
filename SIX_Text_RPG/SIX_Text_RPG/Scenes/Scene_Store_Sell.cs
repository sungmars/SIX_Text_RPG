using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Store_Sell : Scene_Base
    {
        private List<Item> inven = GameManager.Instance.Inventory;

        public override void Awake()
        {
            base.Awake();
            Console.Clear();
            //메뉴추가 
            if (Utils.CursorMenu.Count<= inven.Count)
            {
                for (int i = 0; i < inven.Count; i++)
                {
                    int index = i;
                    if(inven[i].Iteminfo.Price > 0)
                        Utils.CursorMenu.Add(($"[{index + 1}]", () => Resell(index)));
                    else
                        Utils.CursorMenu.Add(($"[{index + 1}]", NotSell));
                }
                Utils.CursorMenu.Add(("[0] 나가기 ", () => Program.CurrentScene = new Scene_Store()));
            }

            //씬 타이틀 인포
            sceneTitle = "송승환 매니저님 방 - 아이템 판매";
            sceneInfo = "인벤토리의 아이템을 팔 수 있습니다.";
        }
        protected override void Display()
        {
            Clear(7, 15);

            Console.SetCursorPosition(1, 7);
            Utils.DisplayCursorMenu(4, 9);
            for (int i = 0; i < inven.Count; i++)
            {
                Item item = inven[i];

                Console.SetCursorPosition(8, 9 + i);
                Console.Write($"{item.Iteminfo.Name}");

                Console.SetCursorPosition(27, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(29, 9 + i);
                Console.Write($"{item.Iteminfo.Description}");

                Console.SetCursorPosition(78, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(80, 9 + i);
                if((float)item.Iteminfo.Price * 0.8f > 0)
                    Console.Write($"{(float)item.Iteminfo.Price * 0.8f} G ");
                else
                    Utils.WriteColor("판매불가",ConsoleColor.Red);
            }
        }

        public override int Update()
        {
            base.Update();
            return 0;
        }

        private void Clear(int min, int max)
        {
            for (int i = min; i < max + 1; i++)
            {
                Utils.ClearLine(0, i);
            }
        }

        private void Resell(int index)
        {
            Item item = inven[index];

            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            float value = (float)item.Iteminfo.Price * 0.8f;
            item.SetBool(ItemBool.IsSold);

            inven.Remove(inven[index]);
            Utils.CursorMenu.Remove(Utils.CursorMenu[index]);
            player.SetStat(Stat.Gold, value, true);
            AudioManager.Instance.Play(AudioClip.SoundFX_Cashier);
        }

        private void NotSell()
        {
            
        }
    }
}
