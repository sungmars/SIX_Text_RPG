namespace SIX_Text_RPG
{
    internal interface ISkill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Mana { get; set; }

        public bool Skill();
    }
}