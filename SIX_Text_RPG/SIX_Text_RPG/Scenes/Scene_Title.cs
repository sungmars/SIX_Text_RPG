namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Title : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();
            Menu.Add("상태 보기");
            Menu.Add("전투 시작");
        }

        public override int Update()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            return Utils.ReadIndex();
        }

        protected override void Display()
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
        }
    }
}