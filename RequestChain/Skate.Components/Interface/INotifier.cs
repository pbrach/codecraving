using System.Collections.Generic;

namespace Components.Interface
{
    public interface INotifier
    {
        void NotifyFriends(IEnumerable<IUser> friends, INewSkillLearned skillLearned);
    }
}