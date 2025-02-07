namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Title : Scene_Base
    {
        public override void Awake()
        {
            sceneTitle = "스파르타 던전에 오신 여러분 환영합니다.";
            sceneInfo = "이제 전투를 시작할 수 있습니다.";
            base.Awake();
            Menu.Add("상태 보기");
            Menu.Add("전투 시작");

            GameManager.Instance.Player = new Player
            {
                Stats = new Stats
                {
                    Name = "Tester",
                    Level = 1,
                    ATK = 10,
                    DEF = 5,
                    MaxHP = 100,
                    HP = 100,
                    Gold = 1500
                }
            };
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentScene = new Scene_PlayerInfo();
                    break;
                case 2:
                    Program.CurrentScene = new Scene_BattleStart();
                    break;
                case 0:
                    Program.CurrentScene = null;
                    break;
            }
            return 0;
        }

        protected override void Display()
        {
            Console.WriteLine(" 원하시는 행동을 입력해주세요.");
        }
    }
}