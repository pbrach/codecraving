using System.Collections.Generic;
using System.Linq;
using Components.Interface;

namespace Components.Implementation
{
    public class User : IUser
    {
        private readonly INotifier _notifier;
        private readonly IList<IUser> _friends;
        private readonly IList<UserSkill> _skills;

        public User(INotifier notifier, string name = "Unknown")
        {
            _notifier = notifier;
            _skills = new List<UserSkill>();
            _friends = new List<IUser>();
            Name = name;
        }

        public string Name { get; }

        public IEnumerable<IUserSkill> Skills => _skills;

        public IEnumerable<IUser> Friends => _friends;


        public void LearnSkill(ISkillType skillType)
        {
            var existingSkill = TryGetExistingSkill(skillType);
            UserSkill incrementedSkill = null;
            if (existingSkill != null)
            {
                existingSkill.Increment();
                incrementedSkill = existingSkill;
            }
            else
            {
                incrementedSkill = new UserSkill(skillType);
                _skills.Add(incrementedSkill);
            }

            var levelName = incrementedSkill.Level.ToString();
            _notifier.NotifyFriends(_friends, new NewSkillLearned(Name, skillType, "Now at level: " + levelName));
        }

        private UserSkill TryGetExistingSkill(ISkillType skillType)
        {
            return _skills.FirstOrDefault(x => x.Skill == skillType);
        }

        public void AddFriend(IUser friend)
        {
            _friends.Add(friend);
        }
    }
}