namespace SIX_Text_RPG
{
    internal class Skill_Critical : ISkill
    {
        public string Name { get; set; } = "크리티컬한 두배 찌르기";
        public string Description { get; set; } = "15% 확률로 두배로 찌릅니다. 이 효과는 스킬에도 적용됩니다.";
        public int Mana { get; set; } = 0;

        public bool Skill()
        {
            return Utils.LuckyMethod(15);
        }
    }
}