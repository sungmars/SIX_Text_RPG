namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Lobby : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();

            sceneTitle = "스파게티 마을";
            sceneInfo =
                "\n               ######   #######  ########  #### ##    ##  ######    ######  ##       ##     ## ########              " +
                "\n  ##   ##     ##    ## ##     ## ##     ##  ##  ###   ## ##    ##  ##    ## ##       ##     ## ##     ##     ##   ## " +
                "\n   ## ##      ##       ##     ## ##     ##  ##  ####  ## ##        ##       ##       ##     ## ##     ##      ## ##  " +
                "\n #########    ##       ##     ## ##     ##  ##  ## ## ## ##   #### ##       ##       ##     ## ########     #########" +
                "\n   ## ##      ##       ##     ## ##     ##  ##  ##  #### ##    ##  ##       ##       ##     ## ##     ##      ## ##  " +
                "\n  ##   ##     ##    ## ##     ## ##     ##  ##  ##   ### ##    ##  ##    ## ##       ##     ## ##     ##     ##   ## " +
                "\n               ######   #######  ########  #### ##    ##  ######    ######  ########  #######  ########            \n";

            Menu.Add("상태 보기");
            Menu.Add("튜터 ZONE");
            zeroText = "게임 종료";
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentScene = new Scene_PlayerInfo();
                    break;
                case 2:
                    // TODO: 주석 해제
                    //Utils.WriteAnim($"{Menu[1]}으로 걸어가는 중...");
                    //Utils.WriteColor(" >> ", ConsoleColor.DarkYellow);
                    //Utils.WriteAnim("탈것이 없어 시간이 지체되는 중...");
                    //Utils.WriteColor(" >> ", ConsoleColor.DarkYellow);
                    //Utils.WriteAnim("뚜벅. 뚜벅. 뚜벅. 뚜벅.");
                    Program.CurrentScene = new Scene_BattleStart();
                    break;
                case 3:
                    Program.CurrentScene = new Scene_Store();
                    break;
                case 0:
                    Utils.Quit();
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Utils.WriteColorLine(" 내일배움캠프에 오신 여러분 환영합니다.", ConsoleColor.DarkCyan);
        }
    }
}