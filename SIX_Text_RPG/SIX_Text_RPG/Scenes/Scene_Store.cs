using SIX_Text_RPG;
using SIX_Text_RPG.Scenes;
using System;
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

        List<Item> storeItem = new List<Item>();

        //아이템사기 델리게이트
        //private Action<int, int> buying;

        //이름, 설명, 체력, 최대체력, 마나, 최대마나, 공격력, 방어력, 값 순
        private (ItemType type, string name, string description, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price)[] itemInfo =
        {

        };



        public override void Awake()
        {
            Menu.Add("아이템 사기");
            Menu.Add("아이템 팔기");
            Menu.Add("도?박");

            sceneTitle = "수상한 매니저님 방";
            sceneInfo = "수상한..? 자세히보니 수상한이 아니라 송승환 매니저님이라 적혀있습니다...";

            //포션 아이템
            SetItem(ItemType.Potion, "최대 체력증가 엘릭서", "최대 체력이 20만큼 증가합니다..", 0f, 20f, 0f, 0f, 0, 0, 0);
            SetItem(ItemType.Potion, "마나 회복포션", "마나를 기존보다 20만큼 회복시켜줍니다.", 0f, 0f, 0f, 20f, 0, 0, 0);
            SetItem(ItemType.Potion, "최대 마나증가 엘릭서", "최대 마나가 20만큼 증가합니다..", 0f, 0f, 0f, 20f, 0, 0, 0);
            SetItem(ItemType.Potion, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0);

            //방어구 아이템 
            SetItem(ItemType.Armor, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0);
            SetItem(ItemType.Armor, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0);
            SetItem(ItemType.Armor, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0);
            SetItem(ItemType.Armor, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0);
            SetItem(ItemType.Armor, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0);


            //무기 아이템
            SetItem(ItemType.Weapon, "강력한 손가락", "체력을 기존보다 20만큼 회복시켜줍니다.", 0f, 0f, 0f, 0f, 0, 5, 0, '☞');
            SetItem(ItemType.Weapon, "펀치", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 10, 0, ' ');
            SetItem(ItemType.Weapon, "물풍선", "추운겨울 튜터님을 시원하게 얼려보아요.", 20f, 0f, 0f, 0f, 0, 0, 0, 'ㄹ');
            SetItem(ItemType.Weapon, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0, ' ');
            SetItem(ItemType.Weapon, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 20f, 0f, 0f, 0f, 0, 0, 0, ' ');


            //악세서리 아이템
            SetItem(ItemType.Accessory, "강력한 손가락", "보기만해도 따끔합니다..", 0f, 0f, 0f, 0f, 0, 0, 0, '☞');
            SetItem(ItemType.Accessory, "펀치", "", 0f, 0f, 0f, 0f, 0, 0, 0, '▶');
            SetItem(ItemType.Accessory, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 0f, 0f, 0f, 0f, 0, 0, 0, ' ');
            SetItem(ItemType.Accessory, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 0f, 0f, 0f, 0f, 0, 0, 0, ' ');
            SetItem(ItemType.Accessory, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 0f, 0f, 0f, 0f, 0, 0, 0, ' ');
            SetItem(ItemType.Accessory, "체력 회복포션", "체력을 기존보다 20만큼 회복시켜줍니다.", 0f, 0f, 0f, 0f, 0, 0, 0, ' ');
        }

        public override int Update()
        {
            switch (base.Update())
            {
                //아이템사기 구현
                case 1:
                    Talking(talkBuy);
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
                    break;
            }
            return 0;
        }

        protected override void Display()
        {

        }

        private void Talking(string[] talk)
        {
            for (int i = 0; i < talk.Length; i++)
            {
                Utils.WriteColor("송승환 매니저님", ConsoleColor.DarkCyan);
                Utils.WriteName(talk[i]);

                Console.ReadLine();
                Console.WriteLine();
            }
        }

        private void Buy()
        {

        }

        //그래픽X
        private void SetItem(ItemType type, string name, string description, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price)
        {
            switch (type) {
                case ItemType.Armor:
                    //아머아이템 생성 후 리스트에 넣기
                    storeItem.Add(new Armor(name, description, hp, maxhp, mp, maxmp, atk, def, price));
                    break;
                case ItemType.Potion:
                    storeItem.Add(new Potion(name, description,hp, maxhp, mp, maxmp, atk, def, price));
                    //포션아이템 생성 후 리스트에 넣기
                    break;
            }
        }

        //그래픽O
        private void SetItem(ItemType type, string name, string description, float hp, float maxhp, float mp, float maxmp, int atk, int def, int price, char graphic)
        {
            switch (type)
            {
                case ItemType.Accessory:
                    storeItem.Add(new Accessory(name, description, hp, maxhp, mp, maxmp, atk, def, price, graphic));
                    //악세서리아이템 생성 후 리스트에 넣기
                    break;
                case ItemType.Weapon:
                    storeItem.Add(new Weapon(name, description, hp, maxhp, mp, maxmp, atk, def, price, graphic));
                    //무기아이템 생성후 리스트에 넣기
                    break;
            }
        }
    }
}

