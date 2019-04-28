using System.Collections.Generic;

namespace Components.Interface
{
    public interface ISkillGroup
    {
        IEnumerable<ISkillType> Skills { get; }
    }
}