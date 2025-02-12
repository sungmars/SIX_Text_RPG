using SIX_Text_RPG.Scenes;

namespace SIX_Text_RPG
{
    internal class Scene_LevelUp : Scene_Base
    {
        public override void Awake()
        {
            base.Awake();

            hasZero = false;
            sceneTitle = "축하합니다! 레벨이 증가했습니다!";

            AudioManager.Instance.Play(AudioClip.Music_LevelUp);
        }

        public override int Update()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return 0;
            }

            Stats stats = player.Stats;
            stats.HP = stats.MaxHP;
            stats.MP = stats.MaxMP;

            // 레벨 증가
            int levelAmount = 0;
            while (stats.EXP >= stats.MaxEXP && stats.MaxEXP > 0)
            {
                if (stats.Level + ++levelAmount > Define.PLAYER_EXP_TABLE.Length)
                {
                    stats.EXP = 0;
                    stats.MaxEXP = 0;
                }
                else
                {
                    stats.EXP -= stats.MaxEXP;
                    stats.MaxEXP = Define.PLAYER_EXP_TABLE[stats.Level];
                }
            }

            // 상태바 갱신을 위한 스탯 적용
            player.Stats = stats;
            Thread.Sleep(1000);

            // 상태바 갱신
            Console.SetCursorPosition(0, 5);
            player.DisplayInfo();

            // 레벨 애니메이션 재생
            player.StatusAnim(Stat.Level, levelAmount);
            stats.Level += levelAmount;

            // 공격력 증가
            float atkAmount = levelAmount * 2.0f;
            player.StatusAnim(Stat.ATK, (int)atkAmount);
            stats.ATK = atkAmount;

            // 방어력 증가
            float defAmount = levelAmount * 1.5f;
            player.StatusAnim(Stat.DEF, (int)defAmount);
            stats.DEF = defAmount;

            // 스탯 적용
            player.Stats = stats;

            Thread.Sleep(2000);
            Program.CurrentScene = new Scene_Lobby();

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
        }
    }
}