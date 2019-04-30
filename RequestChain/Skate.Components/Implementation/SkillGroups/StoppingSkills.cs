using System.Collections.Generic;
using System.Linq;
using Components.Implementation.SkillTypes;
using Components.Interface;

namespace Components.Implementation.SkillGroups
{
    internal class StoppingSkills : ISkillGroup
    {
        public StoppingSkills(IEnumerable<ISkillType> skills)
        {
            Skills = skills.Where(IsStoppingSkill);
        }

        private bool IsStoppingSkill(ISkillType skill)
        {
            var t = skill.GetType();
            switch (skill)
            {
                case HeelBrakeSkill _:
                case DragStopSkill _:
                    return true;
                default:
                    return false;
            }
        }
        
        public IEnumerable<ISkillType> Skills { get; }
    }
}