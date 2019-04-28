using System.Collections.Generic;

namespace Components.Interface
{
    public interface IMessenger
    {
        void NotifyFriends(IEnumerable<IUser> friends, INewSkillLearned skillLearned);
    }
}