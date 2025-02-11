namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleResult : Scene_Base
    {
        private readonly List<Monster> monsters = GameManager.Instance.Monsters;
        private Player? player = GameManager.Instance.Player;
        private float totalDamage = GameManager.Instance.TotalDamage;
        private int currentStage = GameManager.Instance.CurrentStage;


        public override void Awake()
        {
            base.Awake();

            sceneTitle = "Battle!! - Result";
            sceneInfo = "";

            Utils.CursorMenu.Add(("로비로 돌아가기", () => Program.CurrentScene = new Scene_Lobby()));
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
                if(currentStage != 5){
                    //나중에 보스전 추가시 수정
                    GameManager.Instance.TargetStage++;
                }
                int rewardEXP = MonsterRewardEXP();
                int rewardGold = MonsterRewardGold();

                if(rewardEXP < player.Stats.MaxEXP)
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

        private void StageReward()
        {
            //보상
            switch (currentStage)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
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

    }
}