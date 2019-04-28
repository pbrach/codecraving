using System.Collections.Generic;
using Components.Interface;

namespace Components.Implementation.SkillGroups
{
    internal class StrideSkills : ISkillGroup
    {
        public StrideSkills()
        {
            Skills = new List<ISkillType>();
        }

        public StrideSkills(IEnumerable<ISkillType> skills)
        {
            Skills = skills;
        }

        public IEnumerable<ISkillType> Skills { get; }
    }
}