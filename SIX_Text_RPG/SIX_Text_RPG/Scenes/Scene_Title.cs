namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Title : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();
            Menu.Add("상태 보기");
            Menu.Add("전투 시작");
            
            GameManager.Instance.Player = new Player ();
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentScene = new Scene_BattleStart();
                    break;
                case 2:
                    Program.CurrentScene = new Scene_Title();
                    break;
                case 0:
                    Program.CurrentScene = new Scene_Title();
                    break;
            }
            return 0;
        }

        protected override void Display()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
        }
    }
}