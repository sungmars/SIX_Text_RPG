namespace SIX_Text_RPG
{
    internal class Skill_QuadAttack : ISkill
    {
        public string Name { get; set; } = "네번 찌르기";
        public string Description { get; set; } = "튜터님을 네번 찌릅니다, 이 찌르기는 회피할 수 없습니다!";
        public int Mana { get; set; } = 25;

        public bool Skill()
        {
            Player? player = GameManager.Instance.Player;
            return true;
        }
    }
}