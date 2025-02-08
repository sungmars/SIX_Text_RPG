namespace SIX_Text_RPG.Scenes
{
    internal class Scene_Title : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();

            sceneTitle = Define.GAME_TITLE;
            sceneInfo = "" +
                "\n   ######  ########     ###     ######   ##     ## ######## ######## ######## ####    ########  ########   ######  " +
                "\n  ##    ## ##     ##   ## ##   ##    ##  ##     ## ##          ##       ##     ##     ##     ## ##     ## ##    ## " +
                "\n  ##       ##     ##  ##   ##  ##        ##     ## ##          ##       ##     ##     ##     ## ##     ## ##       " +
                "\n   ######  ########  ##     ## ##   #### ######### ######      ##       ##     ##     ########  ########  ##   ####" +
                "\n        ## ##        ######### ##    ##  ##     ## ##          ##       ##     ##     ##   ##   ##        ##    ## " +
                "\n  ##    ## ##        ##     ## ##    ##  ##     ## ##          ##       ##     ##     ##    ##  ##        ##    ## " +
                "\n   ######  ##        ##     ##  ######   ##     ## ########    ##       ##    ####    ##     ## ##         ######\n";

            Menu.Add("새로운 찌르기");
            Menu.Add("익숙한 찌르기");
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentScene = new Scene_CreatePlayer();
                    break;
                case 2:
                    // TODO: 이어하기
                    Scene_CreatePlayer.PlayerName = "테스트플레이어";
                    Program.CurrentScene = new Scene_CreateCharacter();
                    break;
                case 0:
                    Utils.Quit();
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Utils.WriteColorLine(" 누구나 큰일 낼 수 있어!", ConsoleColor.Red);
        }
    }
}