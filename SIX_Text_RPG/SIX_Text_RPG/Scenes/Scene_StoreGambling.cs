namespace SIX_Text_RPG.Scenes
{
    internal class Scene_StoreGambling : Scene_Base
    {
        public static int resultIndex = -1;

        public static float bet;

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

            Utils.CursorMenu.Add(("베팅하기", () =>
            {
                Gambling();
                if (bet != 0) Program.CurrentScene = new Scene_StoreGamblingResult();
            }));
            
            Utils.CursorMenu.Add(("결과창 나가기", () => Program.CurrentScene = new Scene_Store()));
            AudioManager.Instance.Play(AudioClip.Music_Gamble);
        }
        
        protected override void Display()
        {
            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            player.DisplayInfo_Gold();

            Utils.DisplayCursorMenu(5, 10);
        }

        public override int Update()
        {
            base.Update();
            return 0;
        }

        private void Gambling()
        {
            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            Console.SetCursorPosition(4, 10);
            Console.Write(" 베팅금액을 입력해주세요: ");

            //실수값으로 입력
            bool isValid = float.TryParse(Console.ReadLine(), out bet);
            //잘못 입력했을 시 
            if (!isValid || (bet > player.Stats.Gold)|| (bet < 0))
            {
                Console.WriteLine("잘못된 입력입니다.");
                bet = 0;
                Console.ReadKey();
                return;
            }
            resultIndex = Roulette();
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
    }
}
