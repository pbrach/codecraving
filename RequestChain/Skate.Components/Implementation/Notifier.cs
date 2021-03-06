using System;
using System.Collections.Generic;
using Components.Interface;

namespace Components.Implementation
{
    internal class Notifier: INotifier
    {
        public void NotifyFriends(IEnumerable<IUser> friends, INewSkillLearned skillLearned)
        {
            foreach (var friend in friends)
            {
                Console.WriteLine($"Notified {friend.Name} of {skillLearned.LearnerName}'s new skill " +
                                  $"'{skillLearned.NewSkillType.Name}' with message: '{skillLearned.Message}'");
            }
        }
    }
}