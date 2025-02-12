using SIX_Text_RPG.Skills;

namespace SIX_Text_RPG.Scenes
{
    internal class Scene_BattleSkill : Scene_Battle
    {
        private readonly int CURSOR_MENU_X = 30;
        private readonly int CURSOR_MENU_Y = 23;

        public override void Awake()
        {
            hasZero = false;

            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            // 메뉴 청소
            for (int i = 0; i < Utils.CursorMenu.Count; i++)
            {
                Utils.ClearLine(CURSOR_MENU_X - 3, CURSOR_MENU_Y + i);
            }
            Utils.CursorMenu.Clear();

            // 스킬 메뉴 생성
            var skills = player.Skills.Where(skill => skill.Mana > 0).ToList();
            for (int i = 0; i < skills.Count; i++)
            {
                var selectedSkill = skills[i];
                int index = i;
                Utils.CursorMenu.Add(($"{selectedSkill.Name}\tMP {selectedSkill.Mana}", () =>
                {
                    UseSkill(selectedSkill, index);
                }
                ));
            }

            // 스킬 닫기시 메뉴 다 지우고 로비로 돌아가기
            Utils.CursorMenu.Add(("스킬 닫기", () =>
            {
                for (int i = 0; i < Utils.CursorMenu.Count; i++)
                {
                    Utils.ClearLine(CURSOR_MENU_X - 3, CURSOR_MENU_Y + i);
                }
                Utils.CursorMenu.Clear();
                Program.CurrentScene = new Scene_BattleLobby();
            }
            ));
        }

        protected override void Display()
        {
            base.Display();
            Utils.DisplayCursorMenu(CURSOR_MENU_X, CURSOR_MENU_Y);
        }

        public void UseSkill(ISkill skill, int index)
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return;
            }

            if (skill.Skill() == false)
            {
                AudioManager.Instance.Play(AudioClip.SoundFX_Error);
                Console.SetCursorPosition(CURSOR_MENU_X, CURSOR_MENU_Y + index);
                Utils.WriteColor("MP가 부족합니다!", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }

            // 스킬 메뉴 청소
            for (int i = 0; i < Utils.CursorMenu.Count; i++)
            {
                Utils.ClearLine(CURSOR_MENU_X - 3, CURSOR_MENU_Y + i);
            }
            Utils.CursorMenu.Clear();

            // 스킬 예약
            reservedSkill = skill;

            switch (skill)
            {
                case ISkill t1 when t1 is Skill_DoubleAttack:
                case ISkill t2 when t2 is Skill_StrangeAttack:
                    attackCount = 2;
                    break;
                case ISkill t when t is Skill_QuadAttack:
                    attackCount = 4;
                    break;
                case ISkill t when t is Skill_WideAttack:
                    attackCount = 2 * monsters.Count;
                    Program.CurrentScene = new Scene_BattlePhase();
                    return;
                default:
                    return;
            }

            // 몬스터 선택창으로 이동
            Program.CurrentScene = new Scene_BattleSelect();
        }
    }
}