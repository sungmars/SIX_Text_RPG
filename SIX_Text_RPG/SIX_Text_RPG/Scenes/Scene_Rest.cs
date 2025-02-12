namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Rest : Scene_Base
    {
        private Player? player = GameManager.Instance.Player;
        private int cursorX = 50;       // 사진같은거 띄우면 띄울 위치
        private int cursorY = 8;       // 정보창 띄울 위치
        
        public override void Awake()
        {
            base.Awake();
            sceneTitle = "아 개꿀잠";
            sceneInfo = "아 개꿀잠";
        }
        public override int Update()
        {
            switch (base.Update())
            {
                case 0 :
                    Program.CurrentScene = new Scene_Lobby();
                    break;
            }
            return 0;
        }

        protected override void Display()
        {
            // 그림 그리면 그림위치? 안쓰면 지우기
            Console.SetCursorPosition(cursorX ,cursorY);
            Console.Write("그림그리면 위치 테스트");
            Console.SetCursorPosition(1, Console.CursorTop);
            //
            
            //테스트용 테스트 끝나고 지울것
            player.Damaged(50);
            
            
            if (player.Stats.MaxHP == player.Stats.HP)
            {
                Console.SetCursorPosition(1, cursorY+7);
                Utils.WriteColor("나는야 잠만보 풀피여도 잠만 때리지",ConsoleColor.DarkCyan);
            }
            
            Console.SetCursorPosition(1 ,cursorY);
            player.DisplayInfo_Status();                        // ■ ■ ■ ■[][][][] 
            player.StatusAnim(Stat.HP, 100);
            player.SetStat(Stat.HP, 100, true);
            Console.SetCursorPosition(1 ,cursorY);
            player.DisplayInfo_Status();                        // ■ ■ ■ ■ ■ ■ ■ ■
            
        }
        
        
    }
}