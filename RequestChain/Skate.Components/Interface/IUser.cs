using System.Collections.Generic;

namespace Components.Interface
{
    public interface IUser
    {
        string Name { get; }
        IEnumerable<IUserSkill> Skills { get; }
        IEnumerable<IUser> Friends { get; }

        void LearnSkill(ISkillType skillType);
    }
}