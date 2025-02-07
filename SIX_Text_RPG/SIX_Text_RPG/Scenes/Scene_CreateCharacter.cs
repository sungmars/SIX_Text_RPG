namespace SIX_Text_RPG.Scenes
{
    internal class Scene_CreateCharacter : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();

            sceneTitle = $"{Define.GAME_TITLE} 입구";
            sceneInfo =
                "\n  ########  ##     ## ##    ##       ###    ##      ##    ###    ##    ##    ####" +
                "\n  ##     ## ##     ## ###   ##      ## ##   ##  ##  ##   ## ##    ##  ##     ####" +
                "\n  ##     ## ##     ## ####  ##     ##   ##  ##  ##  ##  ##   ##    ####      ####" +
                "\n  ########  ##     ## ## ## ##    ##     ## ##  ##  ## ##     ##    ##        ## " +
                "\n  ##   ##   ##     ## ##  ####    ######### ##  ##  ## #########    ##           " +
                "\n  ##    ##  ##     ## ##   ###    ##     ## ##  ##  ## ##     ##    ##       ####" +
                "\n  ##     ##  #######  ##    ##    ##     ##  ###  ###  ##     ##    ##       ####\n";

            Menu.Add("네! 그럼요! 저는 마계조단입니다.\n마계조단은 자신의 멘탈은 보호하고 튜터님들을 괴롭히는 데 최적화되어 있습니다.\n");
            Menu.Add("이해가 안가네.\n천계조단은 \'찌르기\'에 특화되어 있습니다.");
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    GameManager.Instance.Player = new(PlayerType.마계조단);
                    Program.CurrentScene = new Scene_Lobby();
                    break;
                case 2:
                    GameManager.Instance.Player = new(PlayerType.천계조단);
                    Program.CurrentScene = new Scene_Lobby();
                    break;
                case 0:
                    Utils.Quit();
                    break;
            }

            return 0;
        }

        protected override void Display()
        {
            Utils.WriteColorLine(" 당신은 마계조단입니까?", ConsoleColor.DarkCyan);
        }
    }
}