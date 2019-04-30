using System.Collections.Generic;
using System.Linq;
using Components.Implementation.SkillTypes;
using Components.Interface;

namespace Components.Implementation.SkillGroups
{
    internal class StrideSkills : ISkillGroup
    {


        public StrideSkills(IEnumerable<ISkillType> skills)
        {
            Skills = skills.Where(IsStoppingSkill);
        }

        private bool IsStoppingSkill(ISkillType skill)
        {
            var t = skill.GetType();
            switch (skill)
            {
                case BackwardStrideSkill _:
                case ForwardStrideSkill _:
                    return true;
                default:
                    return false;
            }
        }

        public IEnumerable<ISkillType> Skills { get; }
    }
}