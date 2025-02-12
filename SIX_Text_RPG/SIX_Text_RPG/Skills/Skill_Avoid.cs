namespace SIX_Text_RPG
{
    internal class Skill_Avoid : ISkill
    {
        public string Name { get; set; } = "회피";
        public string Description { get; set; } = "일정 확률로 두 귀를 닫습니다. (데미지 무시)";
        public int Mana { get; set; } = 0;

        public bool Skill()
        {
            return Utils.LuckyMethod(10);
        }
    }
}