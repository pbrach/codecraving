using Components.Interface;

namespace Components.Implementation
{
    internal class UserSkill : IUserSkill
    {
        public UserSkill(ISkillType skill)
        {
            Skill = skill;
            Level = Level.Beginner;
        }
        
        public UserSkill(ISkillType skill, Level level)
        {
            Skill = skill;
            Level = level;
        }

        public ISkillType Skill { get; }
        public Level Level { get; private set; }

        internal void Increment()
        {
            Level = Level + 1;

            if (Level > Level.Professional)
                Level = Level.Professional;
        }
    }
}