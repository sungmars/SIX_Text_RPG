using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleMenu : Scene_Base
    {
        private Random random = new Random();
        private List<Monster> monsters = new List<Monster>();
        public override void Awake()
        {
            base.Awake();
            sceneTitle = "Battle!!";
            sceneInfo = "";

            // 공격 메뉴 추가
            Menu.Add("공격");
            // 아이템 메뉴 추가
            MakeMonster();
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    // 플레이어 공격 씬으로 이동(변경필요)
                    Program.CurrentScene = new Scene_BattleMenu();
                    return 0;
                default:
                    // 존재하지 않는 메뉴(삭제필요) - 씬배이스에 나가기 메뉴 삭제필요?
                    Program.CurrentScene = new Scene_BattleMenu();
                    return 0;
            }
        }

        protected override void Display()
        {
            // 모든 몬스터 정보 출력
            foreach (var monster in monsters)
            {
                monster.DisplayMonster();
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

        // 몬스터 리스트 반환
        public List<Monster> GetMonsters()
        {
            return monsters;
        }
    }
}
