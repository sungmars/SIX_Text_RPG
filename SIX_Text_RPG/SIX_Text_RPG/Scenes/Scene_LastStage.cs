using SIX_Text_RPG.Managers;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_LastStage : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();
            sceneTitle = "한효승 매니저님";
        }

        public override void LateStart()
        {
            AudioManager.Instance.Play(AudioClip.Music_Manager);
            RenderManager.Instance.Play("LastScene", 20, 2, size: 80);
        }

        protected override void Display() { }
    }
}