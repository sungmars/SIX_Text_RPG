﻿using SIX_Text_RPG.Managers;

namespace SIX_Text_RPG.Scenes
{

    internal class Scene_Inventory : Scene_Base
    {
        private bool isEmpty;
        private string displayType;
        private List<Item> Inventory => GameManager.Instance.Inventory;
        public List<Item> Potion
        {
            get { return Inventory.Where(item => item is IConsumable).ToList(); }
        }
        public override void Awake()
        {
            base.Awake();
            sceneTitle = "인벤토리";
            sceneInfo = "Enter키를 눌러 장착/해제 가능합니다.";
        }
        protected override void Display()
        {

            Console.SetCursorPosition(0, 10);
            if (Inventory.Count == 0)//아이템이 없다면 아무것도 없다는 문장과 엔터키를 누르면 나가는 기능
            {
                GagCode();
                isEmpty = true;

                Utils.CursorMenu.Add((
                        "매니저님께 가보자",
                        () =>
                        {
                            Program.CurrentScene = new Scene_Store();
                        }
                ));
                Utils.CursorMenu.Add((
                        "나가기",
                        () =>
                        {
                            Program.CurrentScene = new Scene_Lobby();
                        }
                ));
                Utils.DisplayCursorMenu(5, 15);
            }
            else
            {
                Console.SetCursorPosition(0, 7);
                GameManager.Instance.Player.DisplayInfo(75);
                // 장비아이템, 소모아이템 필터링
                var equipItems = Inventory.Where(item => item is IEquipable).ToList();

                //소비 아이템 출력할곳
                for (int i = 0; i < Potion.Count; i++)
                {
                    var selectItem = Potion[i];
                    string itemName = selectItem.Iteminfo.Name;
                    string itemGraphic = selectItem.Iteminfo.Graphic.ToString();
                    // 앞쪽에서 같은 이름의 아이템이 출력되었는지 확인하기 위해 bool선언
                    bool alreadyPrint = false;
                    for (int j = 0; j < i; j++)//j는 위의 for문 i까지
                    {
                        if (Potion[j].Iteminfo.Name == itemName)
                        {
                            alreadyPrint = true;
                            break;
                        }
                    }
                    if (alreadyPrint)
                    {
                        continue;
                    }//출력됐다면 다음 아이템출력으로 넘어가기하기
                    int count = Inventory.Count(item => item is IConsumable && item.Iteminfo.Name == itemName);//중복개수 세기
                    string displayName = $"{itemGraphic} {selectItem.Iteminfo.Name}" +
                        (count != 0 ? $" -({count})" : "이게 보인다면 버그입니다.");//아이템 갯수 표시
                    Utils.CursorMenu.Add((displayName, () =>
                    {
                        UseItem(Inventory.FindIndex(x => x.Equals(selectItem)));
                    }
                    ));
                }
                Utils.CursorMenu.Add(("〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓〓", () =>//구분선 만듬
                {
                    Utils.WriteColorLine("이거 누르지 마세욧", ConsoleColor.Red);
                    AudioManager.Instance.Play(AudioClip.SoundFX_Error);
                    Console.ReadKey(); // 대기
                }
                ));
                Console.SetCursorPosition(5, 7);
                for (int i = 0; i < equipItems.Count; i++)
                {
                    var selectItem = equipItems[i];
                    string itemGraphic = selectItem.Iteminfo.Graphic.ToString();
                    DisplayType(selectItem);
                    string displayName = $"{itemGraphic} {selectItem.Iteminfo.Name} {displayType}" +
                        (selectItem.Iteminfo.IsEquip ? "[E]" : "");//장착상태 표시해주기
                    Utils.CursorMenu.Add((displayName, () =>
                    {
                        UseItem(Inventory.FindIndex(x => x.Equals(selectItem)));
                    }
                    ));
                }
                Utils.CursorMenu.Add(("나가기", () => { Program.CurrentScene = new Scene_Lobby(); }));
                Utils.DisplayCursorMenu(5, 7, Inventory.Count - Potion.Count + 7, delay: 100);
            }
        }
        public void UseItem(int j)
        {
            var selectItem = Inventory[j];//캡쳐 방지
            IEquipable? equipable = selectItem as IEquipable;
            IConsumable? consumable = selectItem as IConsumable;

            if (consumable != null && consumable.Consume())//소비하는 아이템이라면
            {
                Inventory.Remove(selectItem);//인벤토리 리스트에서 삭제
                AudioManager.Instance.Play(AudioClip.SoundFX_Potion);
            }
            // 장비 아이템 처리
            else if (equipable != null)
            {
                // 아직장착되지 않은 사태면 같은 타입의 아이템이 이미 장착되었는지 확인
                if (!selectItem.Iteminfo.IsEquip)//장착 안되었다면
                {

                    bool sameType = Inventory.Any(item =>//Any는 조건에 해당하는 값이 1개라도 존재한다면 true
                        item is IEquipable &&
                        item.Iteminfo.IsEquip &&
                        item.Type == selectItem.Type);

                    if (sameType)
                    {
                        AudioManager.Instance.Play(AudioClip.SoundFX_Error);
                        Utils.WriteColorLine("같은 타입의 아이템이 이미 장착되어 있습니다!", ConsoleColor.Red);
                        Console.ReadKey();
                        return;
                    }
                    else
                    {
                        AudioManager.Instance.Play(AudioClip.SoundFX_Equip);
                    }
                }
                equipable.Equip();
            }
            else
            {
                Utils.WriteColorLine("착용불가한 아이템", ConsoleColor.Red);
                Console.ReadKey(); // 대기
            }
        }

        public void GagCode()
        {
            string[] gags = new string[]
            {
               "     [현재 가방에 있는 것: 공기]",
               "     [설마 아이템이 있을거라 생각한건 아니죠?]",
               "     [이곳엔 Bug밖에 없는것 같다...]",
               "     [하드웨어 : 사람이 발로 걷어찰 수 있는 컴퓨터의 부분  -Jeff Pesis, 프로그래머-]",
               "     [있었는데, 없었습니다.]",
               "     [아니 그냥 없어요.]",
               "     [무언가 손에 잡혔다. 코를 푼 휴지였다. ]"
            };

            Random random = new Random();
            int randomGag = random.Next(gags.Length);
            Utils.WriteColorLine(gags[randomGag], ConsoleColor.DarkGray);
        }

        public void DisplayType(Item x)//타입 표시필드 내용 정하는 메서드
        {
            if (x.Type == ItemType.Armor)
            {
                displayType = "<방어구>";
            }
            else if (x.Type == ItemType.Accessory)
            {
                displayType = "<장신구>";
            }
            else if (x.Type == ItemType.Weapon)
            {
                displayType = "< 무기 >";
            }
        }

        public override void LateStart()
        {
            base.LateStart();

            if (isEmpty)
            {
                RenderManager.Instance.Play("CodingBird", 75, 7, 50);
            }
        }
    }
}