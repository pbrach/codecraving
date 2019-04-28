using System.Collections.Generic;
using Components.Interface;

namespace Components.Implementation.SkillGroups
{
    internal class StoppingSkills : ISkillGroup
    {
        public StoppingSkills()
        {
            Skills = new List<ISkillType>();
        }

        public StoppingSkills(IEnumerable<ISkillType> skills)
        {
            Skills = skills;
        }

        public IEnumerable<ISkillType> Skills { get; }
    }
}