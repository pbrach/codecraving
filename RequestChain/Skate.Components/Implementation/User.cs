using System.Collections.Generic;
using Components.Interface;

namespace Components.Implementation
{
    public class User : IUser
    {
        private readonly IList<IUser> _friends;

        public User(string name)
        {
            Name = "Unknown";
            Skills = new List<IUserSkill>();
            _friends = new List<IUser>();
            Name = name;
        }

        public string Name { get; }
        public IEnumerable<IUserSkill> Skills { get; }

        public IEnumerable<IUser> Friends => _friends;


        public void LearnSkill(ISkillType skillType)
        {
            
        }

        public void AddFriend(IUser friend)
        {
            _friends.Add(friend);
        }
    }
}