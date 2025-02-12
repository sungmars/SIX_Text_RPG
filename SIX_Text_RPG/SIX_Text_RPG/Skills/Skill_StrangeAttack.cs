namespace SIX_Text_RPG.Skills
{
    internal class Skill_StrangeAttack : ISkill
    {
        public string Name { get; set; } = "어딘가 이상한 찌르기";
        public string Description { get; set; } = "의도가 담긴 찌르기입니다. 상대방이 괴로워 할 수록 회복량이 증가합니다.";
        public int Mana { get; set; } = 10;

        public bool Skill()
        {
            Player? player = GameManager.Instance.Player;
            if (player == null)
            {
                return false;
            }

            if (player.Stats.MP < Mana)
            {
                return false;
            }

            return true;
        }
    }
}