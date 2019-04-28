using Components.Interface;

namespace Components.Implementation
{
    internal class UserSkill : IUserSkill
    {
        public ISkillType Skill { get; }
        public Level Level { get; }
    }
}