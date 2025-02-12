namespace SIX_Text_RPG.Scenes
{
    internal class Scene_StoreBuy : Scene_Base
    {
        // 상점 페이지
        private int cursorX = 0;
        private int cursorY = 0;
        private int priceX = 0;
        private int page = 0;
        private readonly List<Item>[] storeItems = ItemManager.Instance.StoreItems;

        public override void Awake()
        {
            base.Awake();

            //씬 타이틀 인포
            sceneTitle = "송승환 매니저님 방";
            sceneInfo = "다른 아이템이 필요하시다면 ◀, ▶ 방향키를 눌러 보세요!";
        }

        public override int Update()
        {
            int index = base.Update();
            switch (index)
            {
                case -1:    // 왼쪽 페이지
                    page--;
                    UpdateContent();
                    break;
                case 1:     // 오른쪽 페이지
                    page++;
                    UpdateContent();
                    break;
            }

            if (Utils.CursorIndex - 1 == storeItems.Length)
            {
                return 0;
            }

            return -1;
        }

        private void UpdateContent()
        {
            // 초기화
            cursorY = 12;
            priceX = 0;
            page = (page + (int)ItemType.Count) % (int)ItemType.Count;

            // 기존 콘텐츠를 지웁니다.
            Scene_Store.ClearContent();

            // 새 콘텐츠를 출력합니다.
            Console.SetCursorPosition(1, 7);
            switch (page)
            {
                case (int)ItemType.Armor:
                    Console.WriteLine($"[방어구]");
                    break;
                case (int)ItemType.Accessory:
                    Console.WriteLine($"[장신구]");
                    break;
                case (int)ItemType.Weapon:
                    Console.WriteLine($"[ 무기 ]");
                    break;
                case (int)ItemType.Potion:
                    Console.WriteLine($"[ 포션 ]");
                    break;
            }

            for (int i = 0; i < storeItems[page].Count; i++)
            {
                // 스탯 정보 출력 좌표
                cursorX = priceX = 78;
                cursorY++;

                // 아이템이 판매된 상태라면 회색으로 출력하기 위한 변수
                Item item = storeItems[page][i];
                ConsoleColor color = item.Iteminfo.IsSold ? ConsoleColor.DarkGray : ConsoleColor.Gray;

                Console.SetCursorPosition(4, 9 + i);
                Utils.WriteColor(item.Iteminfo.Graphic, color);

                Console.SetCursorPosition(7, 9 + i);
                Utils.WriteColor(item.Iteminfo.Name, color);

                Console.SetCursorPosition(25, 9 + i);
                Utils.WriteColor($"|", color);

                Console.SetCursorPosition(27, 9 + i);
                Utils.WriteColor(item.Iteminfo.Description, color);

                Console.SetCursorPosition(76, 9 + i);
                Utils.WriteColor("|", color);

                if (item.Iteminfo.ATK > 0)
                {
                    Console.SetCursorPosition(cursorX, 9 + i);
                    Utils.WriteColor($"공격력 +{item.Iteminfo.ATK}", color);
                    SetCursorX(i, color);
                }

                if (item.Iteminfo.DEF > 0)
                {
                    Console.SetCursorPosition(cursorX, 9 + i);
                    Utils.WriteColor($"방어력 +{item.Iteminfo.DEF}", color);
                    SetCursorX(i, color);
                }

                if (item.Iteminfo.HP > 0)
                {
                    Console.SetCursorPosition(cursorX, 9 + i);
                    Utils.WriteColor($"체력회복 +{item.Iteminfo.HP}", color);
                    SetCursorX(i, color);
                }

                if (item.Iteminfo.MaxHP > 0)
                {
                    Console.SetCursorPosition(cursorX, 9 + i);
                    Utils.WriteColor($"최대체력 +{item.Iteminfo.MaxHP}", color);
                    SetCursorX(i, color);
                }

                if (item.Iteminfo.MP > 0)
                {
                    Console.SetCursorPosition(cursorX, 9 + i);
                    Utils.WriteColor($"마나재생 +{item.Iteminfo.MP}", color);
                    SetCursorX(i, color);
                }

                if (item.Iteminfo.MaxMP > 0)
                {
                    Console.SetCursorPosition(cursorX, 9 + i);
                    Utils.WriteColor($"최대마다 +{item.Iteminfo.MaxMP}", color);
                    SetCursorX(i, color);
                }
            }

            for (int i = 0; i < storeItems[page].Count; i++)
            {
                Item item = storeItems[page][i];

                Console.SetCursorPosition(priceX, 9 + i);
                if (item.Iteminfo.IsSold)
                {
                    Utils.WriteColor("SOLD OUT", ConsoleColor.Red);
                }
                else
                {
                    Utils.WriteColor($"{item.Iteminfo.Price:N0}G", ConsoleColor.Yellow);
                }
            }

            // 커서 메뉴 생성
            for (int i = 0; i < storeItems[page].Count; i++)
            {
                int index = i;
                Utils.CursorMenu.Add((string.Empty, () => Purchase(index)));
            }

            // 나가기 버튼 생성
            Utils.CursorMenu.Add(("다른 곳 둘러보고 올게요.", () =>
            {
                Program.CurrentScene = new Scene_Lobby();
            }
            ));

            Utils.DisplayCursorMenu(4, 9, storeItems[page].Count + 1);
        }

        private void SetCursorX(int i, ConsoleColor color)
        {
            cursorX += 13;
            Console.SetCursorPosition(cursorX, 9 + i);
            Utils.WriteColor("|", color);
            cursorX += 2;

            priceX = Math.Max(cursorX, priceX);
        }

        protected override void Display()
        {
            Scene_Store.Scripting(ScriptType.StoreBuy);
            UpdateContent();
        }

        private void Purchase(int index)
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            Item item = storeItems[page][index];
            if (item.Iteminfo.IsSold)
            {
                ErrorMessage("이미 구매한 아이템입니다.");
                return;
            }

            if (item.Iteminfo.Price > player.Stats.Gold)
            {
                ErrorMessage("골드가 부족합니다.");
                return;
            }

            if (item.Type != ItemType.Potion)
            {
                item.Sale();
            }

            // 아이템을 구매했습니다.
            player.SetStat(Stat.Gold, -item.Iteminfo.Price, true);
            GameManager.Instance.Inventory.Add(item);

            // 콘텐츠를 갱신합니다.
            UpdateContent();
        }

        private void ErrorMessage(string message)
        {
            AudioManager.Instance.Play(AudioClip.SoundFX_Error);
            Utils.ClearLine(0, cursorY);
            Utils.WriteColor(" >> ", ConsoleColor.DarkYellow);
            Utils.WriteColorLine(message, ConsoleColor.Red);
        }
    }
}