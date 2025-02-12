namespace SIX_Text_RPG.Scenes
{
    internal class Scene_PlayerInfo : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();

            sceneTitle = "상태 보기";
            sceneInfo = "캐릭터의 정보가 표시됩니다.";
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 0:
                    Program.CurrentScene = new Scene_Lobby();
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

            player.DisplayInfo();
            player.DisplayInfo_Skill();
        }
    }
}