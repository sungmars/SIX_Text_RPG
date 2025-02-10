namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleResult : Scene_Base
    {
        private bool isVictory = false;

        public Scene_BattleResult(bool isVictory)
        {
            this.isVictory = isVictory;
        }

        public override void Awake()
        {
            base.Awake();

            sceneTitle = "Battle!! - Result";
            sceneInfo = "";
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 0:
                    GameManager.Instance.TotalDamage = 0f;
                    Program.CurrentScene = new Scene_Title();
                    break;
                default:
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            //이전 체력 계산
            Func<float, float, float> beforeHP = (x, y) =>
                (x + y) > player.Stats.MaxHP ? player.Stats.MaxHP : x + y;

            float oldHP = beforeHP(player.Stats.HP, GameManager.Instance.TotalDamage);
            float newHP = player.Stats.HP;
            //승리 시 
            if (isVictory)
            {
                Utils.WriteColorLine(" Victory", ConsoleColor.Green);
                Console.WriteLine($"\n\n던전에서 몬스터 {GameManager.Instance.Monsters.Count} 마리를 잡았습니다.");
            }

            //패배 시
            else
            {
                Utils.WriteColorLine(" You Lose...", ConsoleColor.DarkRed);
            }


            Console.WriteLine($"\n\n Lv.{player.Stats.Level} {player.Stats.Name}");
            Console.WriteLine($" HP{oldHP} -> {newHP}");

            //데이터 초기화
            GameManager.Instance.TotalDamage = 0f;
            GameManager.Instance.Monsters.Clear();
        }
    }
}