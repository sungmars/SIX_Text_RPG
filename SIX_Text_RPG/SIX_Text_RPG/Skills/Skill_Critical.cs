namespace SIX_Text_RPG
{
    internal class Skill_Critical : ISkill
    {
        public string Name { get; set; } = "크리티컬한 두배 찌르기";
        public string Description { get; set; } = "일정 확률로 두배로 찌릅니다.";
        public int Mana { get; set; } = 0;

        public bool Skill()
        {
            return Utils.LuckyMethod(15);
        }
    }
}