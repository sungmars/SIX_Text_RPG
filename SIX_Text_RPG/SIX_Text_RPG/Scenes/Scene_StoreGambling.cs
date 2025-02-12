namespace SIX_Text_RPG.Scenes
{
    internal class Scene_StoreGambling : Scene_Base
    {
        private int cursorX = 1;
        private int cursorY = 7;

        private int resultIndex = -1;

        private float bet;

        private string[] roulette =
        {
            " 베팅금 전체 몰수 !",
            " 베팅금의 50% 압수 !",
            " 베팅금의 30% 압수 !",
            " 다시 !",
            " 베팅금의 30% 획득 !",
            " 베팅금의 50% 획득 !",
            " 베팅금의 \"두 배\" 획득"
        };


        public override void Awake()
        {
            base.Awake();

            sceneTitle = "수상한..매니저님 방 - 룰렛";
            sceneInfo = "카지노 아닙니다.";

            Menu.Add("베팅하기");
        }

        protected override void Display()
        {

            Scene_Store.Scripting(ScriptType.StoreGamble);

            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            Console.SetCursorPosition(0, 10);
            player.DisplayInfo_Gold();

        }

        public override int Update()
        {
            switch (base.Update())
            {
                //나가기
                case 0:
                    Program.CurrentScene = new Scene_Store();
                    break;

                //업데이트
                case 1:
                    UpdateContent();
                    break;
            }
            return 0;
        }

        private void UpdateContent()
        {
            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;
            for (int i = 7; i < 13; i++)
            {
                if (i == 10) continue;

                Utils.ClearLine(0, i);
            }

            Console.Write(" 베팅금액을 입력해주세요: ");
            bool isValid = float.TryParse(Console.ReadLine(), out bet);
            if (!isValid || (bet > player.Stats.Gold))
            {
                Console.WriteLine("잘못된 입력입니다.");
                Console.ReadKey();
                return;
            }

            resultIndex = Roulette();
            Console.SetCursorPosition(cursorX, cursorY);
            SetResult();
        }

        private int Roulette()
        {
            for (int i = 0; i < roulette.Length; i++)
            {
                if (!Utils.LuckyMethod(60 - (10 * i)))
                {
                    return i;
                }
            }
            return 6;
        }

        //룰렛결과 적용
        private void SetResult()
        {
            float value = 0;
            switch (resultIndex)
            {
                case 0:
                    value = -1f;
                    break;

                case 1:
                    value = -0.5f;
                    break;

                case 2:
                    value = -0.3f;
                    break;

                case 3:
                    value = 0f;
                    break;

                case 4:
                    value = 0.3f;
                    break;

                case 5:
                    value = 1.5f;
                    break;

                case 6:
                    value = 2f;
                    break;
            }
            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;
            float gold = value * bet;

            //출력
            Console.SetCursorPosition(1, 7);
            Console.WriteLine("[게임 결과]");
            Console.WriteLine(roulette[resultIndex]);

            player.StatusAnim(Stat.Gold, (int)gold);
            player.SetStat(Stat.Gold, (int)gold, true);
            Console.ReadKey();
        }
    }
}
