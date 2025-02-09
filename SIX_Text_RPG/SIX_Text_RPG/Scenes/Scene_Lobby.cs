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
            Menu.Add("시간 보기");
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
                    Program.CurrentScene = new Scene_DungeonLobby();
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