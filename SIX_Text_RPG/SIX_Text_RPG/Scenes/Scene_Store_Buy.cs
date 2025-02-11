using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Store_Buy : Scene_Base
    {
        //상점 아이템 리스트
        List<List<Item>> storeItem = new List<List<Item>>();

        private int page = 0;

        public override void Awake()
        {
            ItemInfo[,] iteminfo = Define.ITEM_INFOS;
            //상점에 아이템추가\
            if (iteminfo.Length > storeItem.Count)
            {
                for (int i = 0; i < iteminfo.Length; i++)
                {
                    storeItem.Add(new List<Item>());
                    for (int j = 0; j < iteminfo.GetLength(1); j++)
                    {
                        int num;
                        switch (i)
                        {
                            case (int)ItemType.Armor:
                                num = (int)ItemType.Armor;
                                storeItem[num].Add(new Armor(iteminfo[i, j]));
                                break;

                            case (int)ItemType.Accessory:
                                num = (int)ItemType.Accessory;
                                storeItem[num].Add(new Accessory(iteminfo[i, j]));
                                break;

                            case (int)ItemType.Potion:
                                num = (int)ItemType.Potion;
                                storeItem[num].Add(new Potion(iteminfo[i, j]));
                                break;

                            case (int)ItemType.Weapon:
                                num = (int)ItemType.Weapon;
                                storeItem[num].Add(new Weapon(iteminfo[i, j]));
                                break;
                        }
                    }
                }
            }

            Console.Clear();
            
            //메뉴추가 
            if (Utils.CursorMenu.Count < storeItem[page].Count )
            {
                for (int i = 0; i < storeItem[page].Count; i++)
                {
                    int index = i;
                    Utils.CursorMenu.Add(($"[{i + 1}]", () => Purchase(storeItem[page], index)));
                }
                Utils.CursorMenu.Add(("이제 그만 살게요.", () => Program.CurrentScene = new Scene_Store()));
            }

            //씬 타이틀 인포
            sceneTitle = "송승환 매니저님 방";
            sceneInfo = "아이템을 넘겨보시려면 <-, -> 방향키를 사용해주세요.";
        }

        protected override void Display()
        {
            Clear(7, 15);

            Console.SetCursorPosition(1, 7);
            switch (page)
            {
                case (int)ItemType.Armor:
                    Console.WriteLine($" [방어구]");
                    break;

                case (int)ItemType.Potion:
                    Console.WriteLine($" [ 포션 ]");
                    break;

                case (int)ItemType.Weapon:
                    Console.WriteLine($" [ 무기 ]");
                    break;

                case (int)ItemType.Accessory:
                    Console.WriteLine($" [장신구]");
                    break;
            }

            Utils.DisplayCursorMenu(4, 9);
            for (int i = 0; i < storeItem[page].Count; i++)
            {
                Item item = storeItem[page][i];


                Console.SetCursorPosition(8, 9 + i);
                Console.Write($"{item.Iteminfo.Name}");

                Console.SetCursorPosition(27, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(29, 9 + i);
                Console.Write($"{item.Iteminfo.Description}");

                Console.SetCursorPosition(78, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(80, 9 + i);
                Console.Write($"공격력 +{item.Iteminfo.ATK}");

                Console.SetCursorPosition(90, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(92, 9 + i);
                Console.Write($"방어력 +{item.Iteminfo.DEF}");

                Console.SetCursorPosition(106, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(108, 9 + i);
                if (item.Iteminfo.IsSold)
                {
                    Utils.WriteColor(" 판매완료!", ConsoleColor.Green);
                }
                else
                {
                    Console.Write($"{item.Iteminfo.Price} G");
                }
            }
        }

        public override int Update()
        {
            
            switch (base.Update()) 
            {
                //왼쪽
                case -1:
                    page--;
                    if (page < 0)
                    {
                        page = 3;
                    }
                    break;
                //오른쪽
                case 1:
                    page++;
                    if (page >= storeItem[page].Count)
                    {
                        page = 0;
                    }
                    break;
            }
            return 0;
        }



        //상점 아이템 구매후 인벤토리에 추가해주기
        private void Purchase(List<Item> section, int index)
        {
            Item item = section[index];
            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            if (item.Iteminfo.Price > player.Stats.Gold)
            {
                Utils.WriteColorLine(" Gold 가 부족합니다...", ConsoleColor.DarkRed);
                return;
            }
            else
            {
                if (!item.Iteminfo.IsSold)
                {
                    Utils.WriteColorLine(" 아이템을 구매했습니다 !", ConsoleColor.DarkGreen);
                    GameManager.Instance.Player.SetStat(Stat.Gold, - item.Iteminfo.Price, true);
                    GameManager.Instance.Inventory.Add(item);

                    if (item.Type != ItemType.Potion)
                    {
                        item.Sale();
                    }
                    
                }
            }
            
        }

        //창지우기
        private void Clear(int min, int max)
        {
            for (int i = min; i < max + 1; i++)
            {
                Utils.ClearLine(0, i);
            }
        }
    }
}
