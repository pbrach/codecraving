using System.Collections.Generic;
using System.Linq;
using Components.Implementation.SkillTypes;
using Components.Interface;

namespace Components.Implementation.SkillGroups
{
    internal class CrossoverSkills : ISkillGroup
    {
        public CrossoverSkills(IEnumerable<ISkillType> skills)
        {
            Skills = skills.Where(IsCrossoverSkill);
        }

        private bool IsCrossoverSkill(ISkillType skill)
        {
            var t = skill.GetType();
            switch (skill)
            {
                case LeftTurnForwardCrossoverSkill _:
                case RightTurnForwardCrossoverSkill _:
                    return true;
                default:
                    return false;
            }
        }

        public IEnumerable<ISkillType> Skills { get; }
    }
}