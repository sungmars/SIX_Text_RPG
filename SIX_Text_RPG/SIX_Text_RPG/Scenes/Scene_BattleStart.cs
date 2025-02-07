using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleStart : Scene_Base
    {
        private Random random = new Random();
        private List<Monster> monsters = new List<Monster>();
        Player? player = GameManager.Instance.Player;
        public override void Awake()
        {
            base.Awake();
            hasZero = false;
            sceneTitle = "Battle!!";
            sceneInfo = "";

            // 공격 메뉴 추가
            Menu.Add("공격");
            // 아이템 메뉴 추가
            MakeMonster();
        }

        public override int Update()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            switch (base.Update())
            {
                case 1:
                    Program.CurrentScene = new Scene_PlayerAttack(monsters);
                    return 0;
                default:
                    //1이 아닌 다른 값이 들어오면 씬 이동 없이 다시 메뉴 출력
                    return 1;
            }
        }

        protected override void Display()
        {

            // 모든 몬스터 정보 출력
            foreach (var monster in monsters)
            {
                monster.DisplayMonster();
            }

            Console.WriteLine();
            Console.WriteLine();

            // 플레이어 정보 출력
            if (player != null)
            {
                player.DisplayInfo_Status();
            }
            else
            {
                Console.WriteLine("플레이어 정보가 없습니다.");
            }
        }

        // 랜덤 1-4명의 몬스터 생성
        private void MakeMonster()
        {
            int monsterCount = random.Next(1, 5);
            for (int i = 0; i < monsterCount; i++)
            {
                monsters.Add(SetMonster(random.Next(1, 4)));
            }
        }

        // 3가지 종유릐 입력받은 값의 몬스터 설정
        private Monster SetMonster(int index)
        {
            switch (index)
            {
                case 1:
                    return new Monster(MonsterType.None, "슬라임", 1, 10, 5, 10, 0);
                case 2:
                    return new Monster(MonsterType.None, "고블린", 2, 20, 10, 15, 0);
                case 3:
                    return new Monster(MonsterType.None, "오크", 3, 30, 15, 20, 0);
                default:
                    return new Monster(MonsterType.None, "버그", 100, 10000, 100, 100, 0);
            }
        }
    }
}
