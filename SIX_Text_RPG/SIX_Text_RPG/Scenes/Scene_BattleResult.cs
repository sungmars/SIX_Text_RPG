﻿namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleResult : Scene_Base
    {
        private readonly List<Monster> monsters = GameManager.Instance.Monsters;
        private Player? player = GameManager.Instance.Player;
        private float totalDamage = GameManager.Instance.TotalDamage;
        private float totalUsedMP = GameManager.Instance.TotalUsedMP;
        private int currentStage = GameManager.Instance.CurrentStage;
        private List<Item> rewardItemList = new List<Item>();
        private Random random = new Random();


        public override void Awake()
        {
            base.Awake();

            sceneTitle = "Battle!! - Result";
            sceneInfo = "";


            if (player != null && player.Stats.EXP + MonsterRewardEXP() >= player.Stats.MaxEXP)
            {
                Utils.CursorMenu.Add(("레벨 업!", () => {
                    GameManager.Instance.TotalDamage = 0f;
                    monsters.Clear();
                }
                ));
            }
            else
            {
                Utils.CursorMenu.Add(("로비로 돌아가기", () => {
                    GameManager.Instance.TotalDamage = 0f;
                    monsters.Clear();
                }
                ));
            }


            for (int i = 0; i < 5; i++)
            {
                rewardItemList.Add(new Potion(Define.ITEM_INFOS[(int)ItemType.Potion, i]));
            }

        }

        public override int Update()
        {
            if (base.Update() == 0)
            {
                if (player != null && player.Stats.EXP >= player.Stats.MaxEXP)
                {
                    Program.CurrentScene = new Scene_LevelUp();
                }
                else
                {
                    Program.CurrentScene = new Scene_Lobby();
                }
            }
            return 1;

        }

        protected override void Display()
        {

            if (player == null)
            {
                return;
            }

            //이전 체력 계산
            Func<float, float, float> beforeHP = ((x, y) => (x + y) > player.Stats.MaxHP ? player.Stats.MaxHP : x + y);
            Func<float, float, float> beforeMP = ((x, y) => (x + y) > player.Stats.MaxMP ? player.Stats.MaxMP : x + y);
            float oldHP = beforeHP(player.Stats.HP, totalDamage);
            float newHP = player.Stats.HP;
            float oldMP = beforeMP(player.Stats.MP, totalUsedMP);
            float newMP = player.Stats.MP;
            player.SetStat(Stat.HP, oldHP, false);
            player.SetStat(Stat.MP, oldMP, false);


            int rewardEXP = MonsterRewardEXP();
            int rewardGold = MonsterRewardGold();


            Console.SetCursorPosition(0, 5);
            player.DisplayInfo();


            //승리 시 
            if (monsters.All(monster => monster.IsDead))
            {
                if (currentStage != 5)
                {
                    //나중에 보스전 추가시 수정
                    GameManager.Instance.TargetStage++;
                }
                player.StatusAnim(Stat.EXP, rewardEXP);
                player.SetStat(Stat.EXP, rewardEXP, true);


                player.StatusAnim(Stat.HP, -((int)totalDamage));
                player.SetStat(Stat.HP, newHP, false);

                player.StatusAnim(Stat.MP, -((int)totalUsedMP));
                player.SetStat(Stat.MP, newMP, false);

                player.StatusAnim(Stat.Gold, rewardGold);
                player.SetStat(Stat.Gold, rewardGold, true);

                Console.SetCursorPosition(0, 5);
                player.DisplayInfo();

                Console.WriteLine();
                Utils.WriteColorLine(" 스테이지 클리어!", ConsoleColor.Green);
                Console.WriteLine();
                RewardItems();
                Utils.DisplayLine();


            }

            //패배 시
            else
            {
                Console.WriteLine();
                Utils.WriteColorLine(" 패배", ConsoleColor.DarkRed);
                Console.WriteLine();
                Utils.WriteColorLine(" 저는 이만 정신을 잃고 말았습니다.", ConsoleColor.DarkRed);
                Console.WriteLine();
                Utils.DisplayLine();
                player.SetStat(Stat.HP, 1, false);
            }

            Utils.DisplayCursorMenu(5, 23);

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
            switch (currentStage)
            {
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
                    return rewardItemList[random.Next(1, 3)];
            }
        }

        private void DisplayItemName(Item rewardItme)
        {

            if (rewardItme.Iteminfo.Name.Contains("체력"))
            {
                Utils.WriteColor($" {rewardItme.Iteminfo.Name}", ConsoleColor.Red);
            }
            else if (rewardItme.Iteminfo.Name.Contains("마나"))
            {
                Utils.WriteColor($" {rewardItme.Iteminfo.Name}", ConsoleColor.Blue);
            }
            else if (rewardItme.Iteminfo.Name.Contains("회복약"))
            {
                Utils.WriteColor($" {rewardItme.Iteminfo.Name}", ConsoleColor.Yellow);
            }
        }
    }
}
