using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{

    internal class Scene_Inventory : Scene_Base
    {
        private List<Item> Inventory => GameManager.Instance.Inventory;

        public override void Awake()
        {
            base.Awake();
            sceneTitle = "인벤토리";
            sceneInfo = "Enter키를 눌러 장착/해제 가능합니다.";
            hasZero = false;
            var testWeapon = new Weapon(
     name: "테스트 검",
     desciption: "테스트용으로 만들어진 검입니다.",
     hp: 0,
     maxhp: 0,
     mp: 0,
     maxmp: 0,
     atk: 10,
     def: 0,
     price: 100,
     graphic: '⚔'
 );

            GameManager.Instance.Inventory.Add(testWeapon);
        }


        protected override void Display()
        {
            Console.SetCursorPosition(0, 10);
            if (Inventory.Count == 0)//아이템이 없다면 아무것도 없다는 문장과 엔터키를 누르면 나가는 기능
            {
                Utils.WriteColorLine("현재 가방에 있는 것: 공기", ConsoleColor.DarkGray);
                Utils.CursorMenu.Add((
                        "나가려면 Enter키를 누르세요.", // 메뉴에 출력될 아이템 이름
                        () =>
                        {
                            Program.CurrentScene = new Scene_Lobby();
                        }
                ));
                Utils.DisplayCursorMenu(5, 15);
            }
            else
            {

                // 인벤토리의 커서메뉴 추가
                for (int i = 0; i < Inventory.Count; i++)
                {
                    var selectItem = Inventory[i];//캡쳐 방지

                    string displayName = (selectItem.Iteminfo.IsEquip ? "[E]" : "") + selectItem.Iteminfo.Name;//상태에 따라 표시할 이름 생성

                    Utils.CursorMenu.Add((
                        displayName, // 메뉴에 출력될 아이템 이름
                        () =>
                        {
                            Console.Clear();
                            //장착 상태에 따라 토글
                            IEquipable? equipable = selectItem as IEquipable;
                            if (equipable != null)
                            {
                                equipable.Equip();
                                Program.CurrentScene = new Scene_Inventory();
                            }
                            else
                            {
                                Console.WriteLine("착용불가한 아이템");
                                Console.ReadKey(); // 대기
                            }
                        }
                    ));


                }
                Utils.CursorMenu.Add((
                        "나가기",
                        () =>
                        {
                            Program.CurrentScene = new Scene_Lobby();
                        }
                ));
                Utils.DisplayCursorMenu(5, 7);
            }


        }




    }

}


