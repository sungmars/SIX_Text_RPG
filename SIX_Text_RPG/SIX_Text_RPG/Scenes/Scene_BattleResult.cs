using System;
using System.Runtime.InteropServices;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleResult : Scene_Base
    {
        private readonly List<Monster> monsters = GameManager.Instance.Monsters;
        private Player? player = GameManager.Instance.Player;
        private float totalDamage = GameManager.Instance.TotalDamage;
        private int currentStage = GameManager.Instance.CurrentStage;
        private List<Item> rewardItemList = new List<Item>();
        private Random random = new Random();


        public override void Awake()
        {
            base.Awake();

            sceneTitle = "Battle!! - Result";
            sceneInfo = "";

            Utils.CursorMenu.Add(("로비로 돌아가기", () => Program.CurrentScene = new Scene_Lobby()));

            for (int i = 0; i < 5; i++)
            {
                rewardItemList.Add(new Potion(Define.ITEM_INFOS[(int)ItemType.Potion, i]));
            }
        }

        public override int Update()
        {
            Utils.DisplayCursorMenu(5, 25);

            base.Update();
            if (Program.CurrentScene is Scene_LevelUp)
            {
                return 0;
            }
            return 0;
        }

        protected override void Display()
        {

            if (player == null)
            {
                return;
            }

            Console.SetCursorPosition(0, 5);
            player.DisplayInfo();

            //이전 체력 계산
            Func<float, float, float> beforeHP = ((x, y) => (x + y) > player.Stats.MaxHP ? player.Stats.MaxHP : x + y);

            //승리 시 
            if (monsters.All(monster => monster.IsDead))
            {
                if (currentStage != 5) {
                    //나중에 보스전 추가시 수정
                    GameManager.Instance.TargetStage++;
                }
                int rewardEXP = MonsterRewardEXP();
                int rewardGold = MonsterRewardGold();

                if (rewardEXP < player.Stats.MaxEXP)
                {
                    float oldHP = beforeHP(player.Stats.HP, totalDamage);
                    float newHP = player.Stats.HP;
                    player.SetStat(Stat.HP, oldHP, false);
                    player.StatusAnim(Stat.HP, -((int)totalDamage + 1));
                    player.SetStat(Stat.HP, newHP, false);
                }
                else
                {
                    //레벨업 씬으로 이동 보여주기
                    //player.LevelUp();
                }

                player.StatusAnim(Stat.Gold, rewardGold);
                player.SetStat(Stat.Gold, rewardGold, true);

                RewardItems();

            }

            //패배 시
            else
            {
                Utils.WriteColorLine(" You Lose...", ConsoleColor.DarkRed);
                player.SetStat(Stat.HP, 1, false);
            }

            //데이터 초기화
            GameManager.Instance.TotalDamage = 0f;
            monsters.Clear();
        }


        private int MonsterRewardGold()
        {
            int reward = 0;
            //몬스터 처치 보상
            foreach (var monster in monsters)
            {
                reward += monster.Stats.Gold;
            }
            return reward;
        }

        private int MonsterRewardEXP()
        {
            int reward = 0;
            //몬스터 처치 보상
            foreach (var monster in monsters)
            {
                reward += monster.Stats.EXP;
            }
            return reward;
        }

        private void RewardItems()
        {
            Item rewardItem;
            rewardItem = RandomRewardItem(random.Next(1, 4));
            switch (currentStage) {
                case 0:
                    for (int i = 0; i < 2; i++)
                    {
                        GameManager.Instance.Inventory.Add(rewardItem);
                    }
                    DisplayItemName(rewardItem);
                    Console.WriteLine("을 2개를 획득하였습니다.");
                    break;
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        GameManager.Instance.Inventory.Add(rewardItem);
                    }
                    DisplayItemName(rewardItem);
                    Console.WriteLine("을 4개를 획득하였습니다.");
                    break;
                case 2:
                    for (int i = 0; i < 8; i++)
                    {
                        GameManager.Instance.Inventory.Add(rewardItem);
                    }
                    DisplayItemName(rewardItem);
                    Console.WriteLine("을 8개를 획득하였습니다.");
                    break;
                case 3:
                    for (int i = 0; i < 13; i++)
                    {
                        GameManager.Instance.Inventory.Add(rewardItem);
                    }
                    DisplayItemName(rewardItem);
                    Console.WriteLine("을 13개를 획득하였습니다.");
                    break;
                default:
                    break;
            }
        }

        private Item RandomRewardItem(int choice)
        {
            switch (choice)
            {
                case 1:
                    return rewardItemList[1];
                case 2:
                    return rewardItemList[2];
                case 3:
                    return rewardItemList[4];
                default:
                    return rewardItemList[random.Next(1,3)];
            }
        }

        private void DisplayItemName(Item rewardItme)
        {

            if (rewardItme.Iteminfo.Name.Contains("체력"))
            {
                Utils.WriteColor($"{rewardItme.Iteminfo.Name}", ConsoleColor.Red);
            }
            else if (rewardItme.Iteminfo.Name.Contains("마나"))
            {
                Utils.WriteColor($"{rewardItme.Iteminfo.Name}", ConsoleColor.Blue);
            }
            else if (rewardItme.Iteminfo.Name.Contains("회복약"))
            {
                Utils.WriteColor($"{rewardItme.Iteminfo.Name}", ConsoleColor.Yellow);
            }
        }
    }
}