using SIX_Text_RPG;
using SIX_Text_RPG.Scenes;
using System;
using System.Runtime.ConstrainedExecution;
namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Store : Scene_Base
    {
        private static readonly string[] talkBuy =
        {
            "안녕하세요 name님. 무슨 일 있으신가요?",
            "흠... 아이템을 사러왔다고요?",
            "하.. 이거 참. 아무한테나 보여주는게 아닌데",
            "이거 다른 매니저님들에게는 secret입니다."
        };

        private static readonly string[] talkSell =
        {
            "안녕하세요 name님. 무슨 일 있으신가요?",
            "흠... 아이템을 환불해달라고요?",
            "지금 저한테 buy하신거 맘에 안 드시나요?",
            "이번만 입니다... But 포인트의 80%만 return 해드릴거에요."
        };

        private static readonly string[] talkGambling =
        {
            "안녕하세요 name님. 무슨 일 있으신가요?",
            "흠... 도박을 하러 왔다고요..? 저희 그런 클럽 아닙니다.",
            "다 알고 오셨다니, 어쩔 수 없네요... 이건 다른 매니저님들에게는 secret입니다."
        };

        private static readonly string[] talkFirst =
        {
            "오 name 님 안녕하세요~",
            "여기는 어쩐 일이실까요?"
        };

        //상점 아이템 리스트
        List<List<Item>> storeItem = new List<List<Item>>();
        
        //아이템 속성 리스트(아머, 악세, 포션, 웨폰) 
        List<string> itemType = new List<string>();

        private int left;
        private int top;

        private bool isTalk = false;


        public override void Awake()
        {
            Console.Clear();

            ItemInfo[,] iteminfo = Define.ITEM_INFOS; 

            //메뉴추가 
            Menu.Add("아이템 사기");
            Menu.Add("아이템 팔기");
            Menu.Add("도?박");

            //아이템 종류 메뉴
            {
                itemType.Add("방어구");
                itemType.Add("장신구");
                itemType.Add("포션");
                itemType.Add("무기");
            }


            //상점에 아이템추가
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

            //씬 타이틀 인포
            sceneTitle = "수상한 매니저님 방";
            sceneInfo = "수상한..? 아! 자세히보니 수상한이 아니라 송승환 매니저님이라 적혀있습니다...";
        }

        public override int Update()
        {

            (left, top) = Console.GetCursorPosition();
            Console.SetCursorPosition(left, top);
            switch (base.Update())
            {
                //아이템사기 구현
                case 1:
                    Buy();
                    break;

                //아이템팔기 구현
                case 2:
                    Talking(talkSell);
                    break;

                //도박
                case 3:
                    Talking(talkGambling);
                    break;

                //나가기
                case 0:
                    Program.CurrentScene = new Scene_Lobby();
                    return 0;

                default:
                    break;
            }
            Console.SetCursorPosition(1, 15);
            base.LateStart();
            return -1;
        }

        protected override void Display()
        {
            Talking(talkFirst);
            Console.SetCursorPosition(0, 15);
        }

        private void Talking(string[] talk)
        {
            Console.WriteLine();
            Console.SetCursorPosition(1, 7);
            Utils.WriteColor("송승환 매니저님\n", ConsoleColor.DarkCyan);
            for (int i = 0; i < talk.Length; i++)
            {
                Utils.ClearLine(0, 8);
                Utils.ClearLine(0, 9);
                Console.SetCursorPosition(1, 8);
                Utils.WriteAnim(talk[i]);
                Console.SetCursorPosition(1, 9);
                Utils.WriteColor(">> ", ConsoleColor.DarkYellow);
                Console.ReadLine();
            }
        }

        //내일 버튼작업 꼭하기!!!!!
        //아이템 구매
        private void Buy()
        {
            //대화가 한번만 뜨도록
            if (!isTalk)
            {
                Talking(talkBuy);
                isTalk = true;
            }

            int flag = -9;

            do
            {
                for (int i = 0; i < storeItem.Count; i++)
                {
                    for (int j = 0; j < storeItem[i].Count; j++)
                    {
                        //상점 커서기능
                        Utils.CursorMenu.Add(($"[{j+1}]", () => Purchase(storeItem[i], flag)));
                    }
                    Utils.CursorMenu.Add((" 이제 그만 살게요.", () => flag = 0));
                    ShowItem(i);
                    if (flag == 0) break;
                }
            }
            while (flag != 0);
            Clear(7, 20);
        }

        //상점 아이템 보여주기
        private void ShowItem(int num)
        {
            Clear(7, 20);
            Utils.DisplayCursorMenu(4, 9);
            Console.SetCursorPosition(1, 7);
            switch (num)

                
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
            Console.WriteLine();

            
            for (int i = 0; i < storeItem[num].Count; i++)
            {
                Item item = storeItem[num][i];

                
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
            Console.SetCursorPosition(2, 16);
            Console.WriteLine("  나가기");
            Console.WriteLine("  다음으로 넘기기");
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
                Utils.WriteColorLine(" 아이템을 구매했습니다 !", ConsoleColor.DarkGreen);
                GameManager.Instance.Inventory.Add(item);
            }
            section.RemoveAt(index);
        }


        //줄단위 지우기
        private void Clear(int min, int max)
        {
            for (int i = min; i< max+1; i++)
            {
                Utils.ClearLine(0, i);
            }
        }
    }
}

