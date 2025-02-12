using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_StoreGamblingResult : Scene_Base
    {
        private int resultIndex;
        private float gold;

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

            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            sceneTitle = "수상한..매니저님 방 - 룰렛";
            sceneInfo = "결과를 확인 할 수 있습니다.";

            resultIndex = Scene_StoreGambling.resultIndex;
            bet = Scene_StoreGambling.bet;

            Utils.CursorMenu.Add(("[0] 결과창 나가기", () => Program.CurrentScene = new Scene_StoreGambling()));
        }

        protected override void Display()
        {
            Console.WriteLine(roulette[resultIndex]);

            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            player.DisplayInfo_Gold();

            SetResult();
            Utils.DisplayCursorMenu(5, 10);
        }

        public override int Update()
        {
            switch (base.Update()) 
            {
                case 0:
                    Program.CurrentScene = new Scene_StoreGambling();
                    break;
            }
            return 0;
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
            
            gold = value * bet;

            player.StatusAnim(Stat.Gold, (int)gold);
            player.SetStat(Stat.Gold, (int)gold, true);
        }
    }
}

