namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleResult : Scene_Base
    {
        private bool isVictory = false;
        private int monsterCount = 0;

        public Scene_BattleResult(bool isVictory)
        {
            this.isVictory = isVictory;
        }

        public Scene_BattleResult(bool isVictory, List<Monster> monsters)
        {
            this.isVictory = isVictory;
            this.monsterCount = monsters.Count;
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
                    Program.CurrentScene = new Scene_Title();
                    break;
                default:
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            //이전 체력 계산
            Func<float, float, float> beforeHP = (x, y) =>
                (x + y) > GameManager.Instance.Player.Stats.MaxHP ? GameManager.Instance.Player.Stats.MaxHP : x + y;

            float oldHP = beforeHP(GameManager.Instance.Player.Stats.HP, GameManager.Instance.TotalDamage);
            float newHP = GameManager.Instance.Player.Stats.HP;

            if (isVictory)
            {
                Utils.WriteColorLine(" Victory", ConsoleColor.Green);
                Console.WriteLine($"\n\n던전에서 몬스터 {monsterCount} 마리를 잡았습니다.");
            }
            else
            {
                Utils.WriteColorLine(" You Lose...", ConsoleColor.DarkRed);
            }


            Console.WriteLine($"\n\n Lv.{GameManager.Instance.Player.Stats.Level} {GameManager.Instance.Player.Stats.Name}");
            Console.WriteLine($" HP{oldHP} -> {newHP}");

            GameManager.Instance.TotalDamage = 0f;
        }
    }
}