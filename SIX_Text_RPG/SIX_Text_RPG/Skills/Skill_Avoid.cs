namespace SIX_Text_RPG
{
    internal class Skill_Avoid : ISkill
    {
        public string Name { get; set; } = "회피";
        public string Description { get; set; } = "10% 확률로 피드백을 한 귀로 흘립니다. (데미지 무시)";
        public int Mana { get; set; } = 0;

        public bool Skill()
        {
            return Utils.LuckyMethod(10);
        }
    }
}