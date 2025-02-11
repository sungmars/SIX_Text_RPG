using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Store_Gambling : Scene_Base
    {
        private string[] roulette =
        {
            "베팅금 전체 몰수 !",
            "전 재산의 50% 압수 !",
            "전 재산의 30% 압수 !",
            "다시 !",
            "전재산의 30% 획득 !",
            "전재산의 50% 획득 !",
            "전 재산의 \"두 배\" 획득"
        };
        

        public override void Awake()
        {
            Console.Clear();
            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            sceneTitle = "수상한..매니저님 방 - 룰렛";
            sceneInfo = "카지노 아닙니다.";

            Menu.Add("전재산 베팅");
        }

        protected override void Display()
        {
            if (GameManager.Instance.Player == null) return;
            Player player = GameManager.Instance.Player;

            Console.WriteLine(Console.GetCursorPosition());
            Console.WriteLine($"현재 골드: {player.Stats.Gold}");

            UpdateContent();
        }

        public override int Update()
        {
            switch(base.Update())
            {
                //나가기
                case 0:
                    Program.CurrentScene = new Scene_Store();
                    return 0;

                //업데이트
                case 1:
                    UpdateContent();
                    return -1;
            }
            return -1;
        }
        
        private void UpdateContent()
        {
            int index = Roulette();
            Console.WriteLine(roulette[index]);
            SetResult(index);
        }

        private int Roulette()
        {
            for (int i =0; i< roulette.Length; i++)
            {
                if (!Utils.LuckyMethod(60-(10*i)))
                {
                    return i;
                }
            }
            return 3;
        }

        //룰렛결과 적용
        private void SetResult(int index)
        {
            float value = 0;
            switch (index)
            {
                case 0:
                    value = 0f;
                    break;

                case 1:
                    value = 0.3f;
                    break;

                case 2:
                    value = 0.5f;
                    break;

                case 3:
                    value = 1f;
                    break;

                case 4:
                    value = 1.3f;
                    break;

                case 5:
                    value = 1.5f;
                    break;

                case 6:
                    value = 2f;
                    break;
            }
            if (GameManager.Instance.Player == null) return;

            float gold = value * GameManager.Instance.Player.Stats.Gold;
            GameManager.Instance.Player.SetStat(Stat.Gold, (int)gold);
            //시간되면 애니메이션 넣기
        }
    }
}
