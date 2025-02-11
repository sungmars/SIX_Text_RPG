﻿using SIX_Text_RPG;
using SIX_Text_RPG.Scenes;
using System;
using System.Runtime.ConstrainedExecution;
namespace SIX_Text_RPG.Scenes
{
    public enum ItemType
    {
        Armor,
        Accessory,
        Potion,
        Weapon
    };

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
            "여기는 어쩐 일이실가요?"
        };


        List<List<Item>> storeItem = new List<List<Item>>();
        List<String> itemType = [];

        private int left;
        private int top;

        private bool isTalk = true;

        public override void Awake()
        {
            Console.Clear();

            //메뉴추가 
            Menu.Add("아이템 사기");
            Menu.Add("아이템 팔기");
            Menu.Add("도?박");

            //아이템 종류 메뉴
            itemType.Add("방어구");
            itemType.Add("장신구");
            itemType.Add("포션");
            itemType.Add("무기");
            
            //공간 미리 생성
            for (int i = 0; i < 4; i++)
            {
                storeItem.Add(new List<Item>());
            }

            sceneTitle = "수상한 매니저님 방";
            sceneInfo = "수상한..? 아! 자세히보니 수상한이 아니라 송승환 매니저님이라 적혀있습니다...";

            //포션 아이템
            SetItem(ItemType.Potion, "최대체력 증가 포션", "최대 체력이 20만큼 증가합니다..", 0f, 20f, 0f, 0f, 0, 0, 0);
            SetItem(ItemType.Potion, "마나회복 포션", "마나를 기존보다 20만큼 회복시켜줍니다.", 0f, 0f, 0f, 20f, 0, 0, 0);
            SetItem(ItemType.Potion, "최대마나 증가 포션", "최대 마나가 20만큼 증가합니다..", 0f, 0f, 0f, 20f, 0, 0, 0);
            SetItem(ItemType.Potion, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0);

            //방어구 아이템 
            SetItem(ItemType.Armor, "손가락골무", "찌르는 손가락이 아프지 않도록 지켜주세요.", 0f, 0f, 0f, 0f, 0, 5, 0);
            SetItem(ItemType.Armor, "말랑한 생각", "생각이 말랑해야 정신이 건강합니다...", 0f, 0f, 0f, 0f, 0, 10, 0);
            SetItem(ItemType.Armor, "초랭이", "수생거북 초랭이입니다. 곁에만 있어도 든든합니다", 0f, 0f, 0f, 0f, 0, 17, 0);
            SetItem(ItemType.Armor, "상어옷", "놀랍도록 파랗습니다.. 피부까지도...", 0f, 0f, 0f, 0f, 0, 30, 0);
            SetItem(ItemType.Armor, "노이즈캔슬링", "아무것도 들리지 않습니다..", 0f, 0f, 0f, 0f, 0, 45, 0);

            //무기 아이템
            SetItem(ItemType.Weapon, "강력한 손가락", "보기만 해도 따끔합니다..", 0f, 0f, 0f, 0f, 0, 5, 0, '☞');
            SetItem(ItemType.Weapon, "펀치", "체력을 기존보다 20만큼 회복시켜줍니다.", 0f, 0f, 0f, 0f, 0, 10, 0, '⊃');
            SetItem(ItemType.Weapon, "물풍선", "추운 겨울날 튜터님을 Cool하게 만들어드려요...", 0f, 0f, 0f, 0f, 20, 0, 0, '¤');
            SetItem(ItemType.Weapon, "싸커킥", "아야! 절대절대 \"엉덩이\"를 조심하세요..", 0f, 0f, 0f, 0f, 30, 0, 0, 'ζ');
            SetItem(ItemType.Weapon, "뿅망치", "이거 진짜 아파보이는데... 진짜 뿅망치 맞죠...?", 0f, 0f, 0f, 0f, 45, 0, 0, 'ф');

            ////악세서리 아이템 => 보류
            //SetItem(ItemType.Accessory, "입1", "이가 보이게 활짝웃어봐요", 0f, 0f, 0f, 0f, 0, 0, 0, '∀');
            //SetItem(ItemType.Accessory, "입2", "입을 Woo~아하게 만들어줍니다..", 0f, 0f, 0f, 0f, 0, 0, 0, 'ω');
            //SetItem(ItemType.Accessory, "입3", "네모네모빔을 맞은 코입니다..", 0f, 0f, 0f, 0f, 0, 0, 0, 'ㅱ');
            //SetItem(ItemType.Accessory, "입4", "입을 V자로 웃을 수 있습니다", 0f, 0f, 0f, 0f, 0, 0, 0, 'ⅴ');
            //SetItem(ItemType.Accessory, "입5", "하트모양의 입이 있을 수 있습니다..", 0f, 0f, 0f, 0f, 0, 0, 0, '♥');
            //SetItem(ItemType.Accessory, "입6", "조용히 입을 다물어요..", 0f, 0f, 0f, 0f, 0, 0, 0, 'ⅹ');
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

            int flag = -1;
            do
            {
                for (int i = 0; i < storeItem.Count; i++)
                {
                    ShowItem(i);
                    flag = base.Update();
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

                Console.SetCursorPosition(1, 9 + i);
                Console.Write($" [{i + 1}] ");

                Console.SetCursorPosition(6, 9 + i);
                Console.Write($"{item.Iteminfo.Name}");

                Console.SetCursorPosition(25, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(27, 9 + i);
                Console.Write($"{item.Iteminfo.Description}");

                Console.SetCursorPosition(76, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(78, 9 + i);
                Console.Write($"공격력 +{item.Iteminfo.ATK}");

                Console.SetCursorPosition(88, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(90, 9 + i);
                Console.Write($"방어력 +{item.Iteminfo.DEF}");

                Console.SetCursorPosition(104, 9 + i);
                Console.Write($"|");

                Console.SetCursorPosition(106, 9 + i);
                Console.Write($"{item.Iteminfo.Price} G");
            }
            Console.SetCursorPosition(2, 16);
            Console.WriteLine("[0] 나가기");
        }

        private void Clear(int min, int max)
        {
            for (int i = min; i< max+1; i++)
            {
                Utils.ClearLine(0, i);
            }
        }

        //그래픽X 아이템 추가할 때
        private void SetItem(ItemType type, string name, string description, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price)
        {
            int num;
            switch (type)
            {
                //아머아이템 생성 후 리스트에 넣기
                case ItemType.Armor:
                    num = (int)ItemType.Armor;
                    storeItem[num].Add(new Armor(new ItemInfo()
                    {
                        Name = name,
                        Description = description,
                        HP = hp,
                        MaxHP = maxhp,
                        MP = mp,
                        MaxMP = maxmp,
                        ATK = atk,
                        DEF = def,
                        Price = price
                    }));
                    break;

                //포션아이템 생성 후 리스트에 넣기
                case ItemType.Potion:
                    num = (int)ItemType.Potion;
                    storeItem[num].Add(new Potion(new ItemInfo()
                    {
                        Name = name,
                        Description = description,
                        HP = hp,
                        MaxHP = maxhp,
                        MP = mp,
                        MaxMP = maxmp,
                        ATK = atk,
                        DEF = def,
                        Price = price
                    }));
                    break;
            }
        }

        //그래픽O 아이템 추가할 때
        private void SetItem(ItemType type, string name, string description, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price, char graphic)
        {
            int num;
            switch (type)
            {
                //악세서리아이템 생성 후 리스트에 넣기
                case ItemType.Accessory:
                    num = (int)ItemType.Accessory;
                    storeItem[num].Add(new Accessory(new ItemInfo()
                    {
                        Name = name,
                        Description = description,
                        HP = hp,
                        MaxHP = maxhp,
                        MP = mp,
                        MaxMP = maxmp,
                        ATK = atk,
                        DEF = def,
                        Price = price
                    }, graphic));
                    break;

                //무기아이템 생성후 리스트에 넣기
                case ItemType.Weapon:
                    num = (int)ItemType.Weapon;
                    storeItem[num].Add(new Weapon(new ItemInfo()
                    {
                        Name = name,
                        Description = description,
                        HP = hp,
                        MaxHP = maxhp,
                        MP = mp,
                        MaxMP = maxmp,
                        ATK = atk,
                        DEF = def,
                        Price = price
                    }, graphic));
                    break;
            }
        }

    }
}

