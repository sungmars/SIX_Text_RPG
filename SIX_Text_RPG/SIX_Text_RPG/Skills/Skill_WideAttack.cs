namespace SIX_Text_RPG.Skills
{
    internal class Skill_WideAttack : ISkill
    {
        public string Name { get; set; } = "광역 찌르기\t";
        public string Description { get; set; } = "여기저기 찌르며 희열을 느낍니다. 피해량에 따라 회복량이 증가합니다.";
        public int Mana { get; set; } = 35;

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