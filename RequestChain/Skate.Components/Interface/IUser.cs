using System.Collections.Generic;

namespace Components.Interface
{
    public interface IUser
    {
        string Name { get; }
        IEnumerable<ISkillType> Skills { get; }
        IEnumerable<ISkillGroup> Groups { get; }
        IEnumerable<IUser> Friends { get; }

        void LearnSkill(ISkillType skillType);
    }
}