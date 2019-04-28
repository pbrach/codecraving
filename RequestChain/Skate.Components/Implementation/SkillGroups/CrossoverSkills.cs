using System.Collections.Generic;
using Components.Interface;

namespace Components.Implementation.SkillGroups
{
    internal class CrossoverSkills : ISkillGroup
    {
        public CrossoverSkills()
        {
            Skills = new List<ISkillType>();
        }

        public CrossoverSkills(IEnumerable<ISkillType> skills)
        {
            Skills = skills;
        }

        public IEnumerable<ISkillType> Skills { get; }
    }
}